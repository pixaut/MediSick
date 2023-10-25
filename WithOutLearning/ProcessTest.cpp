#include "..\Library\NeuronNetwork.h"
#include <fstream>
#include <algorithm>
#include <utility>

int main(){

    std::ifstream fin;

    char UserId[256];                                                       /* userid*/
    int n,c;                                                                /* quantity of simphtones, c - number of symphtone*/
                                                                            /* 50 - number of input values*/

    int layers;

     char* PathToNetworkDescr= "..\\NetworkDescription\\NetworkSize.txt";
     char* PathToNetwork = "..\\NetworkDescription\\Network.txt";
     char* PathToIn = "..\\Telegram server\\bin\\Debug\\net7.0\\Inputuser\\input.txt";
     char* PathToOut = "..\\Telegram server\\bin\\Debug\\net7.0\\Outputuser\\output.txt";

    fin.open(PathToNetworkDescr);                                           //
    fin >> layers;                                                          //
    int* size = new int[layers];                                            //
    for(int i = 0;i < layers;i++){                                          //          Descript Network
        fin >> size[i];                                                     //
    }                                                                       //
    fin.close();                                                            //

    double input[size[0]];   

    NeuronNetwork nn(layers,size);                                          //                                            
    nn.LoadNetwork(PathToNetwork);                                          //

    fin.open(PathToIn);                                                     //
    
    std::fill(input,input+size[0],0.0);

    //fin >> UserId;                                                        //
    fin >> n;                                                               //
    for(int i = 0;i < n;i++){                                               //
        fin >> c;                                                           //  Input simphtones
        input[c-1] = 1.0;                                                   //
    }                                                                       //
    fin.close();                                                            //
    nn.SetInput(input);


    nn.ForwardFeed();                                                       //
    std::ofstream fout(PathToOut);  
    
    /*double* SF = nn.SoftMax();
    std::pair<double,int> ans[size[layers-1]];
    
    for(int i = 0;i < size[layers-1];i++){
        ans[i] = {SF[i],i+1};
    }

    std::sort(ans,ans+size[layers-1]);
    std::reverse(ans,ans+size[layers-1]);

    for(int i = 0;i < size[layers-1];i++){
        fout << ans[i].second << " - " << ans[i].first << '\n';
    }
                                         */
    fout << nn.Predict()+1;                                                 // do output <<  UserId << ' ' 
    fout.close();                                                           //

    return 0;
}
