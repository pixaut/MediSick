#include "..\Library\NeuronNetwork.h"
#include <fstream>
#include <iostream>
#include <iomanip>

int main(){

    std::ifstream fin;

    int layers,N,n,c;
    char PathToTests[] = "..\\TrainingTests\\RandomTests.txt";
    char NetworkPath[] = "..\\Network\\Network.txt";
    char NetworkSizePath[] = "..\\Network\\NetworkSize.txt";
    char gender;
    double s = 0.0,e = 0.0,SpeedOfLearning = 0.3;


    fin.open(NetworkSizePath);
    fin >> layers;                                          //
    int* size = new int[layers];                         
    for(int i = 0;i < layers;i++){                          //      
        fin >> size[i];                                     //
    }                                                       //  Descript Network
    fin.close();                                            //
    
    

    int WomanIndex = size[0];
    int ManIndex = size[0]+1;
    int NoneIndex = size[0]+1;

    size[0] += 3;
    NeuronNetwork nn(layers,size);                          //
    nn.SetRandom();                                         //
    
    double *input = new double[size[0]];
    double *rightanswer = new double[size[layers-1]];
    std::fill(rightanswer,rightanswer+size[layers-1],0.0);

    bool Svt = false;

    while(s < 98.0){

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

            fin >> gender;
            fin >> n;
            for(int i = 0;i < n;i++){
                fin >> c;
                input[c-1] = 1.0;
            }
            fin >> c;
            rightanswer[c-1] = 1.0;
            
            input[WomanIndex] = (double)(gender == 'w');
            input[ManIndex] = (double)(gender == 'm');

            if(gender == 'n'){
                if(rand()%2 == 0){
                    input[WomanIndex] = 1.0;
                }else{
                    input[ManIndex] = 1.0;
                }
            }

            nn.SetInput(input);
            nn.ForwardFeed();
            nn.BackPropagation(rightanswer,SpeedOfLearning);

            e += nn.ErrorCouter(rightanswer);

            rightanswer[c-1] = 0.0;
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