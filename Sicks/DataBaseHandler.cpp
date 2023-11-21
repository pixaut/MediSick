#include <iostream>
#include <sstream>
#include <fstream>
#include <vector>
#include <string>
#include <algorithm>

int main()
{
    std::vector< std::vector<std::string> > array;
    std::ifstream fin("Sicks.csv");
    std::ofstream fout("SicksList.txt");
    std::string line,field,s,name;
    std::stringstream ss;
    int n = 0,k = 0;

    getline(fin,line);
    ss = std::stringstream(line);
    while(getline(ss,s,',') ){
        n = std::max(n,atoi(s.c_str()) );
    }
    fout << n << ' ';

    array.resize(n);


    getline(fin,line);

    ss = std::stringstream(line);
    k = 0;

    getline(ss,field,',');
    getline(ss,field,',');

    for(k = 0;k < n;k++){
        getline(ss,field,',');
        array[k].push_back(field);
    }


    char gender[n];
    k = 0;
    while ( getline(fin,line) )
    {
        ss = std::stringstream(line);

        getline(ss,field,',');//trash
        getline(ss,name,',');//trash
        for(int i = 0;i < n;i++)
        {
            getline(ss,field,',');
            if(field == "1")array[i].push_back(std::to_string(k+1));
            if(field == "н")gender[i] = 'n';
            if(field == "м")gender[i] = 'm';
            if(field == "ж")gender[i] = 'w';
        }
 
        k++;
    }

    fout << k-2 << '\n';

    for(int i = 0;i < n;i++){


        fout << gender[i] << ' ' << array[i].size()-1 << '\t';
        if(array[i].size()-1 <= 9) fout << '\t';

        for(int j = 1;j < array[i].size();j++){
            if(j == k-2) continue;
            fout << array[i][j] << ' ';
        }
        
        fout << i+1 << '\n';
    }

}