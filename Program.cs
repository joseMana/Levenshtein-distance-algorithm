using System;

class Program
{
    static void Main()
    {
        // Sample list of available options
        string[] availableOptions = { "table", "chair", "desk", "lamp", "book" };

        // User input
        string userInput = "tvble";

        // Find the closest match
        string closestMatch = FindClosestMatch(userInput, availableOptions);

        // Check if the similarity is above a certain threshold
        if (CalculateSimilarity(userInput, closestMatch) > 0.7)
        {
            Console.WriteLine($"Did you mean '{closestMatch}'?");
        }
        else
        {
            Console.WriteLine("No match found.");
        }
    }

    static string FindClosestMatch(string input, string[] options)
    {
        int minDistance = int.MaxValue;
        string closestMatch = "";

        foreach (var option in options)
        {
            int distance = CalculateLevenshteinDistance(input, option);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestMatch = option;
            }
        }

        return closestMatch;
    }

    static int CalculateLevenshteinDistance(string s1, string s2)
    {
        int[,] distance = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 0; i <= s1.Length; i++)
            distance[i, 0] = i;

        for (int j = 0; j <= s2.Length; j++)
            distance[0, j] = j;

        for (int i = 1; i <= s1.Length; i++)
        {
            for (int j = 1; j <= s2.Length; j++)
            {
                int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;

                distance[i, j] = Math.Min(
                    Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost
                );
            }
        }

        return distance[s1.Length, s2.Length];
    }

    static double CalculateSimilarity(string s1, string s2)
    {
        int maxLength = Math.Max(s1.Length, s2.Length);
        if (maxLength == 0)
        {
            return 1.0; // Both strings are empty
        }

        int distance = CalculateLevenshteinDistance(s1, s2);
        return 1.0 - (double)distance / maxLength;
    }
}
