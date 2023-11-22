#include <fstream>
#include <vector>
#include <iostream>


int main(){

    /* program that update input and output layers numbers */

    const char *NetworkPath = "..\\Network\\NetworkSize.txt";
    const char *SicksPath = "..\\Sicks\\SicksList.txt";
    std::ifstream fin;
    std::ofstream fout;
    std::vector<int> v;
    int n,m,N,c;
    

    fin.open(SicksPath);
    fin >> n >> m; // read size of output and input layers
    fin.close();

    
    fin.open(NetworkPath);
    fin >> N; // read number of layers
    for(int i = 0;i < N;i++){
        fin >> c; // read layers
        v.push_back(c);
    }
    fin.close();

    
    fout.open(NetworkPath);
    fout << N << '\n' << m << ' '; // write number of layers and input layer

    for(int i = 1;i < N-1;i++){
        fout << v[i] << ' ';  // write old layers
    }
    fout << n; //write output layer

    fout.close();
}