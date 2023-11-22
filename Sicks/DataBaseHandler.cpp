#include <iostream>
#include <sstream>
#include <fstream>
#include <vector>
#include <string>
#include <algorithm>

int main()
{

    /* Handling the tablet (file csv type) of sicks and symphtones */

    std::vector< std::vector<std::string> > array;
    std::ifstream fin("Sicks.csv");
    std::ofstream fout("SicksList.txt");
    std::string line,field,s;
    std::stringstream ss;
    int n = 0,k = 0;

    getline(fin,line);
    ss = std::stringstream(line);
    while(getline(ss,s,',') ){
        n = std::max(n,atoi(s.c_str()) ); // finding number of sicks
    }

    array.resize(n);


    getline(fin,line);

    ss = std::stringstream(line);

    getline(ss,field,',');
    getline(ss,field,',');

    for(int i = 0;i < n;i++){
        getline(ss,field,',');
        array[i].push_back(field);
    }


    char gender[n];
    while ( getline(fin,line) )
    {
        ss = std::stringstream(line); // translate string to string stream type for getline function

        getline(ss,field,',');
        getline(ss,field,',');
        for(int i = 0;i < n;i++)
        {
            getline(ss,field,',');
            if(field == "1")array[i].push_back(std::to_string(k+1));

            /* correlating field as gender */
            if(field == "н")gender[i] = 'n';
            if(field == "м")gender[i] = 'm';
            if(field == "ж")gender[i] = 'w';
        }
 
        k++;
    }

    fout << n << ' '  << k-2 << '\n'; // writing number of sicks and symphtones (k-2 because gender and number of symptones not symphtones)

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