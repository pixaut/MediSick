#include <fstream>
#include <vector>
#include <iostream>


int main(){

    char *NetworkPath = "..\\Network\\NetworkSize.txt";

    std::ifstream fin("..\\Sicks\\SicksList.txt");
    std::ofstream fout;
    int n,m,N,c;
    fin >> n >> m;
    fin.close();

    std::vector<int> v;
    fin.open(NetworkPath);


    fin >> N;
    for(int i = 0;i < N;i++){
        fin >> c;
        v.push_back(c);
    }
    fin.close();

    
    fout.open(NetworkPath);
    fout << N << '\n' << m << ' ';

    for(int i = 1;i < N-1;i++){
        fout << v[i] << ' ';
    }
    fout << n;

    fout.close();
}