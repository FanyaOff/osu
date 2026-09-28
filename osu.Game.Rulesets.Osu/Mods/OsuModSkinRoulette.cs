// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Threading;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Scoring;
using osu.Game.Scoring;
using osu.Game.Skinning;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModSkinRoulette : Mod, IApplicableToScoreProcessor, IApplicableToSkinManager, IDisallowScoreSubmission, IHasModIconScale
    {
        public override string Name => "Skin Roulette";

        public override string Acronym => "SR";

        public override LocalisableString Description => "A fresh look at every combo milestone.";

        public override IconUsage? Icon => FontAwesome.Solid.Random;

        public float IconScale => 0.62f;

        public override ModType Type => ModType.Fun;

        public override bool Ranked => false;

        public override bool ValidForMultiplayer => false;

        public override bool ValidForMultiplayerAsFreeMod => false;

        [SettingSource(
            "Combo interval",
            "Change to the next available skin each time this combo milestone is reached.",
            SettingControlType = typeof(SettingsSlider<int, SkinRouletteComboSlider>)
        )]
        public BindableInt ComboInterval { get; } = new BindableInt(50)
        {
            MinValue = 30,
            MaxValue = 200,
        };

        [SettingSource(
            "Repeat skins",
            "Allow a skin to be selected again before every available skin has been used."
        )]
        public BindableBool RepeatSkins { get; } = new BindableBool(false);

        private readonly BindableInt currentCombo = new BindableInt();

        private SkinManager? skinManager;
        private int nextSkinChangeCombo;

        public void ApplyToSkinManager(SkinManager skinManager, CancellationToken cancellationToken)
        {
            this.skinManager = skinManager;
            skinManager.StartSkinRoulette(cancellationToken, RepeatSkins.Value);
        }

        public void ApplyToScoreProcessor(ScoreProcessor scoreProcessor)
        {
            nextSkinChangeCombo = ComboInterval.Value;
            currentCombo.BindTo(scoreProcessor.Combo);
            currentCombo.BindValueChanged(combo =>
            {
                if (combo.NewValue <= combo.OldValue)
                {
                    nextSkinChangeCombo = ComboInterval.Value;
                    return;
                }

                if (combo.NewValue >= nextSkinChangeCombo && skinManager?.SelectNextPreloadedSkin() == true)
                {
                    // If background preloading was not ready at the exact milestone, schedule the
                    // following switch relative to the combo at which this one actually occurred.
                    nextSkinChangeCombo = combo.NewValue + ComboInterval.Value;
                }
            }, true);
        }

        public ScoreRank AdjustRank(ScoreRank rank, double accuracy) => rank;
    }

    public partial class SkinRouletteComboSlider : RoundedSliderBar<int>
    {
        public SkinRouletteComboSlider()
        {
            KeyboardStep = 1;
        }
    }
}
