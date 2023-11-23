#include "..\Library\NeuronNetwork.h"
#include <fstream>
#include <iostream>
#include <iomanip>

int main(){

    std::ifstream fin;

    int layers,N,n,c,WomanIndex,ManIndex;
    const char *PathToTests     = "..\\TrainingTests\\RandomTests.txt";
    const char *NetworkPath     = "..\\Network\\Network.txt";
    const char *NetworkSizePath = "..\\Network\\NetworkSize.txt";
    char gender;
    double s = 0.0,e = 0.0,SpeedOfLearning = 0.6;


    /* reading network size*/

    fin.open(NetworkSizePath);
    fin >> layers;                                          
    int* size = new int[layers];                            
    for(int i = 0;i < layers;i++){                              
        fin >> size[i];                                     
    }                                                       
    fin.close();                                            
    
    /* setting index of woman and man flag*/
    WomanIndex = size[0];
    ManIndex = size[0]+1;

    size[0] += 2;

    /* construct network*/

    NeuronNetwork nn(layers,size);                                                           
    double *input = new double[size[0]];
    double *rightanswer = new double[size[layers-1]];
    std::fill(rightanswer,rightanswer+size[layers-1],0.0);

    bool Svt = false; // flag for buttons

    fin.open("ButtonNew.txt"); // button that switch training from old for new weights
    fin >> Svt;
    if(Svt) nn.LoadNetwork(NetworkPath);
    fin.close();
    

    Svt = false;

    while(s < 98.5){ // s - percent of correct that wouldn't be high

        s = 0.0,e = 0.0;
        fin.open("Button.txt"); // button that save network forcibly
        fin >> Svt;
        if(Svt) nn.SaveNetwork(NetworkPath);
        fin.close();

        

        /* algorithm that training network from batch that has been generated */

        fin.open(PathToTests);
        fin >> N; // size of batch

        for(int k = 0;k < N;k++){
            std::fill(input,input+size[0],0.0); // clearing input for correct work

            /* input 1 test from batch */

            fin >> gender >> n;
            for(int i = 0;i < n;i++){
                fin >> c;
                input[c-1] = 1.0;
            }
            fin >> c;
            rightanswer[c-1] = 1.0; // set right answer index element as 1
            input[WomanIndex] = (double)(gender == 'w');
            input[ManIndex]   = (double)(gender == 'm');

            /* processing test */

            nn.SetInput(input);
            nn.ForwardFeed();

            /* learning from errors */

            nn.BackPropagation(rightanswer,SpeedOfLearning);
            e += nn.ErrorCouter(rightanswer);

            rightanswer[c-1] = 0.0;// cleaning right answer index for new generations
            s += (nn.Predict() == c-1);
            
        }

        fin.close();

        s = s/N*100.0 , e /= N; // avaraging percent of correct and error
        std::cout << std::fixed << std::setprecision(8) << s << '\t' << e << '\n'; // writing debugging data to console 

    }

    nn.SaveNetwork(NetworkPath); // saving network that has been trained

    delete[] input,rightanswer,size; //freeing memory
    return 0;
}