#include <iostream>
#include <sstream>
#include <fstream>
#include <vector>
#include <string>

int main()
{
    std::vector< std::vector<int> > array;
    std::ifstream fin("Sicks.csv");
    std::ofstream fout("SicksList.txt");
    std::string line,field,s;
    std::stringstream ss;
    int n = 0,k = 0;

    getline(fin,line);
    ss = std::stringstream(line);
    while(getline(ss,s,',') ){
        n = std::max(n,atoi(s.c_str()) );
    }
    fout << n << ' ';

    array.resize(n);


    getline(fin,line);//trash


    char gender[n];

    while ( getline(fin,line) )
    {
        ss = std::stringstream(line);

        getline(ss,field,',');//trash
        getline(ss,field,',');//trash

        for(int i = 0;i < n;i++)
        {
            getline(ss,field,',');
            if(field == "1")array[i].push_back(k);
            if(field == "н")gender[i] = 'n';
            if(field == "м")gender[i] = 'm';
            if(field == "ж")gender[i] = 'w';
        }
 
        k++;
    }

    fout << k-2 << '\n';

    for(int i = 0;i < n;i++){

        fout << gender[i] << ' ' << array[i].size() << '\t';

        for(int j = 0;j < array[i].size();j++){
            if(j == k-2) continue;
            fout << array[i][j]+1 << ' ';
        }
        
        fout << i+1 << '\n';
    }

}