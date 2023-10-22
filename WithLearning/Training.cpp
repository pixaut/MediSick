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
    double s = 0.0,e = 0.0,SpeedOfLearning = 0.4;


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

    bool Svt = false;

    while(s < 95.0){

        s = 0.0,e = 0.0;

        fin.open("Button.txt");

        fin >> Svt;

        fin.close();

        if(Svt){
            nn.SaveNetwork(NetworkPath);
        }

        fin.open(PathToTests);
        
        //std::cout << bool(fin);

        fin >> N;
        
        for(int k = 0;k < N;k++){
            std::fill(input,input+size[0],0.0);

            fin >> n;
            for(int i = 0;i < n;i++){
                fin >> c;
                input[c-1] = 1.0;
            }
            fin >> c;
            rightanswer[c] = 1.0;
            
            nn.SetInput(input);
            nn.ForwardFeed();
            nn.BackPropagation(rightanswer,SpeedOfLearning);

            e += nn.ErrorCouter(rightanswer);

            rightanswer[c] = 0.0;
            if(nn.Predict() == c){
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