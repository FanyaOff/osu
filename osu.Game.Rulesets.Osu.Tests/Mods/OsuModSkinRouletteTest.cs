// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using NUnit.Framework;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Mods;
using osu.Game.Utils;

namespace osu.Game.Rulesets.Osu.Tests.Mods
{
    [TestFixture]
    public class OsuModSkinRouletteTest
    {
        [Test]
        public void TestMetadataAndLimits()
        {
            var mod = new OsuModSkinRoulette();

            Assert.Multiple(() =>
            {
                Assert.That(mod.Acronym, Is.EqualTo("SR"));
                Assert.That(mod.ComboInterval.Value, Is.EqualTo(50));
                Assert.That(mod.ComboInterval.MinValue, Is.EqualTo(30));
                Assert.That(mod.ComboInterval.MaxValue, Is.EqualTo(200));
                Assert.That(mod.RepeatSkins.Value, Is.False);
                Assert.That(mod.IconScale, Is.EqualTo(0.62f));
                Assert.That(mod.Ranked, Is.False);
                Assert.That(mod.ValidForMultiplayer, Is.False);
                Assert.That(mod.ValidForMultiplayerAsFreeMod, Is.False);
                Assert.That(mod, Is.InstanceOf<IDisallowScoreSubmission>());
            });
        }

        [Test]
        public void TestRegisteredAsFunMod()
        {
            Assert.That(new OsuRuleset().GetModsFor(ModType.Fun).SelectMany(ModUtils.FlattenMod),
                Has.Some.InstanceOf<OsuModSkinRoulette>());
        }
    }
}
