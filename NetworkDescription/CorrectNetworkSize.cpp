#include <fstream>
#include <iostream>

int main(){

    std::ifstream fin;
    std::ofstream fout("NetworkSize.txt");
   
    char s[256];
    int n = 0,m = 0,N,c;

    fin.open("..\\TrainingTests\\DescriptionOfDeseas.txt");
    while(fin >> s){
        if(s[0] >= 'A' && s[0] <= 'Z') n++;
    }
    fin.close();

    fin.open("..\\TrainingTests\\DescriptionOfSimphtones.txt");
    while(fin >> s){
        if(s[0] >= 'A' && s[0] <= 'Z') m++;
    }
    fin.close();

    std::cout << "Number of layers: ";
    std::cin >> N;

    fout << N << '\n';
    fout << m << ' ';

    for(int i = 0;i < N-2;i++){
        std::cout << "Number of neurons in hidden layer " << i+1 << " : ";
        std::cin >> c;
        fout << c << ' ';
    }

    fout << n;

    std::cout << "Thanks!";

}