/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row

        while (!reader.EndOfData)
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            if (players.ContainsKey(playerId))
                players[playerId] += points;
            else
                players[playerId] = points;
        }

        var sorted = players
            .OrderByDescending(kvp => kvp.Value)
            .ToArray();

        // Top 10 IDs (starter code hinted at this)
        var topPlayers = new string[Math.Min(10, sorted.Length)];
        for (int i = 0; i < topPlayers.Length; i++)
            topPlayers[i] = sorted[i].Key;

        // Display table
        Console.WriteLine("Top 10 Players by Career Points (Player ID)");
        Console.WriteLine("Rank\tPlayerId\tPoints");
        for (int i = 0; i < topPlayers.Length; i++)
        {
            Console.WriteLine($"{i + 1}\t{sorted[i].Key}\t\t{sorted[i].Value}");
        }
    }
}
