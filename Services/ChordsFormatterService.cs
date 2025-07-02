using System.Text.RegularExpressions;

namespace GospelReachCapstone.Services
{
    public class ChordsFormatterService
    {
        private static readonly string[] Chromatic = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        public string BracketChords(string LyricsWithChords)
        {
            // Regex for unbracketed chords: start of line or space, then chord, then space or end
            return LyricsWithChords = Regex.Replace(LyricsWithChords,
                @"(?<=^|\s)([A-G](#|b)?(m|maj|min|dim|aug|sus|add)?\d*(b5)?(\/[A-G](#|b)?)?)(?=\s|$)",
                match => $"[{match.Value}]");
        }

        public string RemoveBrackets(string LyricsWithChords)
        {
            // Remove brackets around any chord-like text
            return LyricsWithChords = Regex.Replace(LyricsWithChords, @"\[(?<chord>[A-G](#|b)?(m|maj|min|dim|aug|sus|add)?\d*(b5)?(\/[A-G](#|b)?)?)\]", "${chord}");
        }

        public string TransposeAll(string lyricsWithChords, int step, bool removeBrackets = false)
        {
            var processed = Regex.Replace(lyricsWithChords, @"\[(?<chord>[A-G](#|b)?(m|maj|min|dim|aug|sus|add)?\d*(b5)?(\/[A-G](#|b)?)?)\]", match =>
            {
                string chord = match.Groups["chord"].Value;
                string transposed = TransposeChord(chord, step);
                return $"[{transposed}]";
            });

            if (removeBrackets)
            {
                processed = Regex.Replace(processed, @"\[(?<chord>[^\]]+)\]", "${chord}");
            }

            return processed;
        }

        public string TransposeChord(string chord, int step)
        {
            var match = Regex.Match(chord, @"^(?<root>[A-G](#|b)?)(?<ext>[^/]*)?(\/(?<bass>[A-G](#|b)?))?$");
            if (!match.Success) return chord;

            string root = NormalizeToSharp(match.Groups["root"].Value);
            string extension = match.Groups["ext"].Value;
            string? bass = match.Groups["bass"].Success ? NormalizeToSharp(match.Groups["bass"].Value) : null;

            string newRoot = TransposeNote(root, step);
            string? newBass = bass != null ? TransposeNote(bass, step) : null;

            return $"{newRoot}{extension}{(newBass != null ? "/" + newBass : "")}";
        }

        private string NormalizeToSharp(string note)
        {
            return note switch
            {
                "Db" => "C#",
                "Eb" => "D#",
                "Gb" => "F#",
                "Ab" => "G#",
                "Bb" => "A#",
                _ => note
            };
        }

        private string TransposeNote(string note, int step)
        {
            int index = Array.IndexOf(Chromatic, note);
            if (index < 0) return note;
            return Chromatic[(index + step + 12) % 12];
        }
    }
}
