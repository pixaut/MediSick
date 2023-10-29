#include <fstream>
#include <vector>
#include <utility>
#include <iostream>

int main(){

    std::ifstream fin("SicksTablet.txt");
    std::ofstream fout("SicksList.txt");
    char trash[10000],gender;
    int n,m,c;

    fin >> n >> m;
    fin.getline(trash,10000,'\n');

    std::vector<std::vector<int> > v;
    v.resize(n);

    for(int i = 0;i < m;i++){
        fin.getline(trash,256,'|');
        for(int j = 0;j < n;j++){
            fin >> c;
            if(c) v[j].push_back(i);
        }
    }

    fin.getline(trash,256,'|');

    fout << n << '\n';
    for(int i = 0;i < n;i++){
        fin >> gender;//gender
        fout << gender << ' ' <<v[i].size() << '\t';
        for(auto j:v[i]){
            fout << j+1 << ' ';
        }
        fout << i+1 << '\n';
    }




}