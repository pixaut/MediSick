#include "..\Library\NeuronNetwork.h"
#include <fstream>
#include <iostream>
#include <iomanip>

int main(){

    std::ifstream fin;

    int layers,N,n,c;
    char PathToTests[] = "..\\TrainingTests\\RandomTests.txt";
    char NetworkPath[] = "..\\NetworkDescription\\Network.txt";
    char NetworkSizePath[] = "..\\NetworkDescription\\NetworkSize.txt";
    double s = 0.0,e = 0.0,SpeedOfLearning = 0.1;


    fin.open(NetworkSizePath);
    fin >> layers;                                          //
    int* size = new int[layers];                         
    for(int i = 0;i < layers;i++){                          //      
        fin >> size[i];                                     //
    }                                                       //  Descript Network
    fin.close();                                            //
    
    NeuronNetwork nn(layers,size);                          //
    nn.SetRandom();                                         //

    
    double *input = new double[size[0]];
    std::fill(input,input+size[0],0.0);

    double *rightanswer = new double[size[layers-1]];
    std::fill(rightanswer,rightanswer+size[layers-1],0.0);
    

    while(s < 90.0){
        
        s = 0.0,e = 0.0;

        fin.open(PathToTests);

        fin >> N;

        for(int k = 0;k < N;k++){
            std::fill(input,input+size[0],0.0);

            fin >> n;
            for(int i = 0;i < n;i++){
                fin >> c;
                input[c] = 1.0;
            }
            fin >> c;
            rightanswer[c] = 1.0;

            nn.SetInput(input);
            nn.ForwardFeed();
            nn.BackPropogation(rightanswer,SpeedOfLearning);
            e += nn.ErrorCouter(rightanswer);

            rightanswer[c] = 0.0;
            
            if(nn.Predict() == c){
                //std::cout << nn.Predict() << ' ';
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