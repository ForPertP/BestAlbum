#include <string>
#include <vector>
#include <unordered_map>
#include <algorithm>

using namespace std;

vector<int> solution(vector<string> genres, vector<int> plays) {
    unordered_map<string, int> total;
    unordered_map<string, vector<pair<int,int>>> songs;
    total.reserve(genres.size());
    
    for (int i = 0; i < genres.size(); i++) {
        total[genres[i]] += plays[i];
        songs[genres[i]].push_back({plays[i], i});
    }
    
    vector<pair<string,int>> order(total.begin(), total.end());
    sort(order.begin(), order.end(),
         [](const auto& a, const auto& b) {
             return a.second > b.second;
         });
    
    vector<int> answer;
    for (const auto& p : order) {
        auto& v = songs[p.first];
        sort(v.begin(), v.end(),
             [](const auto& a, const auto& b) {
                 return a.first == b.first ? 
                     a.second < b.second : a.first > b.first;
             });
        
        for (int i = 0; i < v.size() && i < 2; i++)
            answer.push_back(v[i].second);
    }
    
    return answer;
}


vector<int> solution(vector<string> genres, vector<int> plays) {
    unordered_map<string, int> total;
    unordered_map<string, vector<pair<int,int>>> songs;
    
    for (int i = 0; i < genres.size(); i++) {
        total[genres[i]] += plays[i];
        songs[genres[i]].push_back({-plays[i], i});
    }
    
    vector<pair<int,string>> order;
    order.reserve(total.size());
    for (auto& p : total)
        order.push_back({-p.second, p.first});
    
    sort(order.begin(), order.end());
    
    vector<int> answer;
    for (const auto& p : order) {
        auto& v = songs[p.second];
        sort(v.begin(), v.end());
        for (int i = 0; i < v.size() && i < 2; i++)
            answer.push_back(v[i].second);
    }
    
    return answer;
}
