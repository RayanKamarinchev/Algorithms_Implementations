#include <iostream>
#include <vector>
using namespace std;

std::vector<std::string> split(std::string s, std::string delimiter) {
    size_t pos_start = 0, pos_end, delim_len = delimiter.length();
    std::string token;
    std::vector<std::string> res;

    while ((pos_end = s.find(delimiter, pos_start)) != std::string::npos) {
        token = s.substr(pos_start, pos_end - pos_start);
        pos_start = pos_end + delim_len;
        res.push_back(token);
    }

    res.push_back(s.substr(pos_start));
    return res;
}

int main()
{
    //11:45 5min
    int t;
    cin >> t;
    for (int i = 0; i < t; i++)
    {

    }
    string str;
    cin >> str;
    string delimiter = " ";
    vector<std::string> v = split(str, delimiter);


    return 0;
}