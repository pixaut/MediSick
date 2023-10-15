#include <vector>
#include <iostream>
#include <fstream>
#include <ctime>
#include <unordered_set>
#include <utility>

std::vector<std::pair<int,std::pair<int,int> > > deseas;
std::vector <int> ans;
std::unordered_set <int> Set;

std::vector<int> GenerateRandomVector(int j,int n){

    ans.clear();
    Set.clear();

    int Max,Min;

    std::tie(Min,Max) = deseas[j].second;

    while(Set.size() < n){
        Set.insert(rand()%Max+Min);
    }

    for(auto i:Set){
        ans.push_back(i);
    }

    return ans;
}

int main(){

    srand(time(nullptr));

    int n;
    std::ifstream fin("DescriptionOfDeseas.txt");
    std::ofstream fout("RandomTests.txt");

    char s[256];

    while(fin >> s >> n){
        static int j = 1;
        deseas.push_back({n,{j,j+n-1} });
        j++;
    }
    fin.close();

    std::cout << "Number of tests: ";
    std::cin >> n;
    std::vector<int> RV;
    int j,m;

    fout << n << '\n';
    for(int i = 0;i < n;i++){

        j = rand()%(deseas.size());
        m = rand()%(deseas[j].first)+1;

        //std::cout << j << ' ' << m << '\n';

       // std::cout << j << ' ' << m << '\n';

        RV = GenerateRandomVector(j,m);

        fout << m << '\t';
        
        for(int k = 0;k < RV.size();k++){
            //std::cout << RV[k] << ' ';
            fout << RV[k] << ' ';
        }

        //std::cout << "\n\n";

        fout << j << '\n'; 

    }

}