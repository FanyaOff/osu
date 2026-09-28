// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Threading;
using osu.Game.Skinning;

namespace osu.Game.Rulesets.Mods
{
    /// <summary>
    /// An interface for mods which need to control the active skin during gameplay.
    /// </summary>
    public interface IApplicableToSkinManager : IApplicableMod
    {
        void ApplyToSkinManager(SkinManager skinManager, CancellationToken cancellationToken);
    }
}
