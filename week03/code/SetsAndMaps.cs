using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var results = new List<string>();

        foreach (var w in words)
        {
            if (string.IsNullOrEmpty(w) || w.Length != 2)
                continue;

            // Special case: identical letters like "aa" should never match
            if (w[0] == w[1])
            {
                // still add (harmless) so we don't repeat work if duplicates exist
                seen.Add(w);
                continue;
            }

            // If duplicates exist (perf test does), ignore repeats safely
            if (!seen.Add(w))
                continue;

            // Reverse the two letters
            var rev = new string(new[] { w[1], w[0] });

            if (seen.Contains(rev))
            {
                results.Add($"{w} & {rev}");
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length < 4) continue;

            var degree = fields[3].Trim();

            if (degrees.ContainsKey(degree))
                degrees[degree]++;
            else
                degrees[degree] = 1;
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Important Note: Ignore spaces and ignore case.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        var counts = new Dictionary<char, int>();

        // Count letters in word1
        foreach (char c in word1)
        {
            if (c == ' ') continue;
            char key = char.ToLowerInvariant(c);

            if (counts.ContainsKey(key))
                counts[key]++;
            else
                counts[key] = 1;
        }

        // Subtract letters in word2
        foreach (char c in word2)
        {
            if (c == ' ') continue;
            char key = char.ToLowerInvariant(c);

            if (!counts.TryGetValue(key, out int val))
                return false;

            val--;
            if (val == 0)
                counts.Remove(key);
            else
                counts[key] = val;
        }

        return counts.Count == 0;
    }

    /// <summary>
    /// Reads earthquake JSON data from USGS and returns formatted strings:
    /// "place - Mag magnitude"
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        if (featureCollection?.Features == null || featureCollection.Features.Count == 0)
            return [];

        var results = new List<string>(featureCollection.Features.Count);

        foreach (var f in featureCollection.Features)
        {
            var place = f?.Properties?.Place ?? "Unknown location";
            var mag = f?.Properties?.Mag;

            string magText = mag.HasValue ? mag.Value.ToString() : "N/A";
            results.Add($"{place} - Mag {magText}");
        }

        return results.ToArray();
    }
}
