using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int[] solution(string[] genres, int[] plays) {
        
        Dictionary<string, int> total = new Dictionary<string, int>();
        Dictionary<string, List<Tuple<int, int>>> songs 
            = new Dictionary<string, List<Tuple<int, int>>>();

        for (int i = 0; i < genres.Length; i++) {
            if (!total.ContainsKey(genres[i])) {
                total[genres[i]] = 0;
                songs[genres[i]] = new List<Tuple<int, int>>();
            }

            total[genres[i]] += plays[i];
            songs[genres[i]].Add(new Tuple<int, int>(plays[i], i));
        }

        var orderedGenres = total
            .OrderByDescending(x => x.Value)
            .ToList();

        List<int> answer = new List<int>();

        foreach (var genre in orderedGenres) {
            var sortedSongs = songs[genre.Key]
                .OrderByDescending(x => x.Item1)
                .ThenBy(x => x.Item2)
                .Take(2);

            foreach (var song in sortedSongs) {
                answer.Add(song.Item2);
            }
        }

        return answer.ToArray();
    }
}


public class Solution {
    public int[] solution(string[] genres, int[] plays) {
        
        Dictionary<string, int> total = new Dictionary<string, int>();
        Dictionary<string, List<Tuple<int, int>>> songs 
            = new Dictionary<string, List<Tuple<int, int>>>();

        for (int i = 0; i < genres.Length; i++) {
            if (!total.ContainsKey(genres[i])) {
                total[genres[i]] = 0;
                songs[genres[i]] = new List<Tuple<int, int>>();
            }

            total[genres[i]] += plays[i];
            
            songs[genres[i]].Add(new Tuple<int, int>(-plays[i], i));
        }

        List<Tuple<int, string>> order = new List<Tuple<int, string>>();
        foreach (var p in total) {
            order.Add(new Tuple<int, string>(-p.Value, p.Key));
        }

        order.Sort((a, b) => {
            int cmp = a.Item1.CompareTo(b.Item1);
            if (cmp == 0)
                return string.Compare(a.Item2, b.Item2, StringComparison.Ordinal);
            return cmp;
        });

        List<int> answer = new List<int>();

        foreach (var p in order) {
            var list = songs[p.Item2];

            list.Sort((a, b) => {
                int cmp = a.Item1.CompareTo(b.Item1);
                if (cmp == 0)
                    return a.Item2.CompareTo(b.Item2);
                return cmp;
            });

            for (int i = 0; i < list.Count && i < 2; i++) {
                answer.Add(list[i].Item2);
            }
        }

        return answer.ToArray();
    }
}
