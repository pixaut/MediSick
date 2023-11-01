#include <iostream>
#include <sstream>
#include <fstream>
#include <vector>
#include <string>

int main()
{
    std::ifstream fin("Sicks.csv");
    std::ofstream fout("SicksList.txt");

    std::string line, field;
 
    std::vector< std::vector<int> > array;
    
    getline(fin,line);

    std::stringstream ss(line);
    std::string s;
    
    int n = 0;
    while(getline(ss,s,',') ){
        n = std::max(n,atoi(s.c_str()) );
    }
    fout << n << '\n';

    array.resize(n);

    getline(fin,line);

    int k = 0;
    std::string gender[n];

    while ( getline(fin,line) )
    {
        std::stringstream ss(line);

        getline(ss,field,',');
        getline(ss,field,',');

        for(int i = 0;i < n;i++)
        {
            getline(ss,field,',');
            if(field == "1")array[i].push_back(k);
            if(field == "н" || field == "м" || field == "ж"){
                gender[i] = field;
            } 
        }
 
        k++;
    }


    for(int i = 0;i < array.size();i++){

        if(gender[i]  == "н")fout << "n";
        if(gender[i]  == "м")fout << "m";
        if(gender[i]  == "ж")fout << "w";

        fout << ' ' << array[i].size() << '\t';

        for(int j = 0;j < array[i].size();j++){
            if(j == k-2) continue;
            fout << array[i][j]+1 << ' ';
        }
        
        
        fout << i+1 << '\n';
    }

}