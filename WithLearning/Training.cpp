#include "..\Library\NeuronNetwork.h"
#include <fstream>
#include <iostream>
#include <iomanip>

int main(){

    std::ifstream fin;

    int layers;

    fin.open("..\\NetworkDescription\\NetworkSize.txt");

    fin >> layers;                                          //
    int* size = new int[layers];                         
    for(int i = 0;i < layers;i++){                          //      
        fin >> size[i];                                     //
    }                                                       //  Descript Network
    fin.close();                                            //
    
    NeuronNetwork nn(layers,size);                          //
    nn.SetRandom();                                         //

    char PathToTests[] = "..\\TrainingTests\\RandomTests.txt";
    char NetworkPath[] = "..\\NetworkDescription\\Network.txt";
    double s = 0.0,SpeedOfLearning = 0,e = 0.0;
    double *input = new double[size[0]];
    double *rightanswer = new double[size[layers-1]];

    std::fill(rightanswer,rightanswer+size[layers-1],0.0);
    std::fill(input,input+size[layers-1],0.0);

    while(s < 90.0){
        
        s = 0.0,e = 0.0;

        int N;
        fin.open(PathToTests);

        //std::cout << bool(fin) << ' ';
        fin >> N;

        for(int k = 0;k < N;k++){
            int n,c,answer;
            fin >> n;
            for(int i = 0;i < n;i++){
                fin >> c;
                input[c-1] = 1.0;
            }
            fin >> c;
            rightanswer[c-1] = 1.0;

            nn.SetInput(input);
            nn.ForwardFeed();
            nn.BackPropogation(rightanswer,SpeedOfLearning);
            e += nn.ErrorCouter(rightanswer);

            //if(k == N-1) nn.SaveNetwork(NetworkPath);

            rightanswer[c-1] = 1.0;
            if(nn.Predict() == c-1){
                s += 1.0;
            }
        }

        fin.close();

        s = s/N*100.0;
        e /= N;

        std::cout << std::fixed << std::setprecision(8) << s << '\t' << e << '\n';

    }

    nn.SaveNetwork(NetworkPath);

    delete[] input,rightanswer,size;
    return 0;
}