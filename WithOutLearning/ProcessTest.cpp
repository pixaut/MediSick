#include "..\Library\NeuronNetwork.h"
#include <fstream>


int main(){

    std::ifstream fin;

    char UserId[30];                                                        // userid
    int n,c;                                                                // quantity of simphtones, c - number of symphtone
    double input[50];                                                       // 50 - number of input values

    int layers;

    fin.open("..\\NetworkDescription\\NetworkSize.txt");                    //
    fin >> layers;                                                          //
    int* size = new int[layers];                                            //
    for(int i = 0;i < layers;i++){                                          //          Descript Network
        fin >> size[i];                                                     //
    }                                                                       //
    fin.close();                                                            //
                                                                            
    NeuronNetwork nn(layers,size);                                          //                                            
    nn.LoadNetwork("..\\NetworkDescription\\weights.txt");                  //

    fin.open("input.txt");                                                  //

    fin >> UserId;                                                          //
    fin >> n;                                                               //
    for(int i = 0;i < n;i++){                                               //
        fin >> c;                                                           //  Input simphtones
        input[c-1] = 1.0;                                                   //
    }                                                                       //
    fin.close();                                                            //
    nn.SetInput(input);
    //sadasdasda



    nn.ForwardFeed();                                                       //
    std::ofstream fout("output.txt");                                       //
    fout << UserId << ' ' << nn.Predict();                                  // do output
    fout.close();                                                           //

    return 0;
}
