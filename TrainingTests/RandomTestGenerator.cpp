#include <vector>
#include <iostream>
#include <fstream>
#include <ctime>
#include <unordered_set>
#include <utility>

int n,m,c;
std::string ans;
std::vector< std::vector<int> > v;
std::unordered_set<int> st;

std::string GetTest(){

    ans.clear();
    st.clear();
    int k = rand()%m;
    int t = rand()%(v[k].size()/2)+v[k].size()/2;

    ans += std::to_string(t) + '\t';

    while(st.size() < t){
        st.insert(rand()%v[k].size());
    }

    for(auto i:st){
        ans += std::to_string(i) + ' ';
    }

    ans += '\t' + std::to_string(k);

    return ans;
}

int main(){

    srand(time(NULL));

    std::ifstream fin("DescriptionOfSimphtones.txt");
    std::ofstream fout("RandomTests.txt");

    fin >> n >> m;

    v.resize(m);

    for(int i = 0;i < n;i++){
        for(int j = 0;j < m;j++){
            fin >> c;
            if(c)v[j].push_back(i+1); 
        }
    }
    fin.close();

    int N;

    std::cout << "Enter number of tests: ";
    std::cin >> N;


    fout << N << '\n';
    for(int i = 0;i < N;i++){
        fout << GetTest() << '\n';
    }

    fout.close();


}