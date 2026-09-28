// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Game.Rulesets.Mods
{
    /// <summary>
    /// Allows a mod icon with unusually large glyph bounds to opt into visual size correction.
    /// </summary>
    public interface IHasModIconScale
    {
        float IconScale { get; }
    }
}
