#include "..\Library\NeuronNetwork.h"
#include <fstream>


int main(){

    std::ifstream fin;
    std::ofstream fout;

    char UserId[30],PathToNetwork[] = "..\\NetworkDescription\\Network.txt";                                                        // userid
    int n,c,layers;                                                                // quantity of simphtones, c - number of symphtone
    double input[50];                                                       // 50 - number of input values

    std::fill(input,input+50,0.0);

    fin.open("..\\NetworkDescription\\NetworkSize.txt");                    //
    fin >> layers;                                                          //
    int* size = new int[layers];                                            //
    for(int i = 0;i < layers;i++){                                          //          Descript Network
        fin >> size[i];                                                     //
    }                                                                       //
    fin.close();                                                            //
                                                                            
    NeuronNetwork nn(layers,size);                                          //                                            
    nn.LoadNetwork(PathToNetwork);                  

    fin.open("input.txt");                                                  //

    fin >> UserId;                                                          //
    fin >> n;                                                               //
    for(int i = 0;i < n;i++){                                               //
        fin >> c;                                                           //  Input simphtones
        input[c-1] = 1.0;                                                   //
    }                                                                       //
    fin.close();                                                            //
    nn.SetInput(input);

    nn.ForwardFeed();
                                                           
    fout.open("output.txt");                                       
    fout << UserId << ' ' << nn.Predict()+1;                                // do output
    fout.close();                                                           //

    return 0;
}
