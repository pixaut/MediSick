#include "..\Library\NeuronNetwork.h"
#include <fstream>
#include <utility>
#include <algorithm>
/*WARNING gender update*/
int main(){

    std::ifstream fin;                         
    int n,c,layers;                                                         // quantity of simphtones, c - number of symphtone
    char wm[16];
    bool IsMan = true;


    fin.open("..\\Network\\NetworkSize.txt");                    
    fin >> layers;                                                          //
    int* size = new int[layers];                                            //
    for(int i = 0;i < layers;i++){                                          //          Descript Network
        fin >> size[i];                                                     //
    }                                                                       //
    fin.close();                                                            //

    // size[0]++;

    double input[size[0]];   

    NeuronNetwork nn(layers,size);                                                                                      
    nn.LoadNetwork("..\\Network\\Network.txt");                  

    fin.open("..\\Telegram server\\bin\\Debug\\net7.0\\InOutUser\\input.txt");                                                  
    
    //fin >> wm;

    // IsMan = (wm == "m");
    // input[size[0]] = double(int(IsMan));

    std::fill(input,input+size[0],0.0);

    fin >> n;                                                               //
    for(int i = 0;i < n;i++){                                               //
        fin >> c;                                                           //  Input simphtones
        input[c-1] = 1.0;                                                   //
    }                                                                       //
    fin.close();                                                            //
    nn.SetInput(input);



    nn.ForwardFeed();                                                       
    std::ofstream fout("..\\Telegram server\\bin\\Debug\\net7.0\\InOutUser\\output.txt");                                       
    fout << nn.Predict()+1;// << '\n';
    
    // double* SF = nn.SoftMax();
    // std::pair<double,int> ans[size[layers-1]];

    // for(int i = 0;i < size[layers-1];i++){
    //     ans[i] = {SF[i],i+1};
    // }
    
    // std::sort(ans,ans+size[layers-1]);
    // std::reverse(ans,ans+size[layers-1]);

    // for(int i = 0;i < 10;i++){
    //     fout << ans[i].second << " - " << ans[i].first << '\n';
    // }
    
                                                     // do output
    fout.close();                                                           //

    return 0;
}
