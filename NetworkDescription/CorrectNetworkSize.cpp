#include <fstream>
#include <iostream>

int main(){

    std::ifstream fin("..\\TrainingTests\\DescriptionOfSimphtones.txt");
    std::ofstream fout("NetworkSize.txt");
   
    char s[256];
    int n,m,N,c;

    fin >> n >> m;
    fin.close();

    std::cout << "Number of layers: ";
    std::cin >> N;

    fout << N << '\n';
    fout << n << ' ';

    for(int i = 0;i < N-2;i++){
        std::cout << "Number of neurons in hidden layer " << i+1 << " : ";
        std::cin >> c;
        fout << c << ' ';
    }

    fout << m;

    std::cout << "Thanks!";

}