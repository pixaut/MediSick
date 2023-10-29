#include "..\Library\NeuronNetwork.h"
#include <fstream>
#include <utility>
#include <algorithm>
/*WARNING gender update*/
int main(){

    std::ifstream fin;                         
    int n,c,layers;                                                         // quantity of simphtones, c - number of symphtone
    char gender;


    fin.open("..\\Network\\NetworkSize.txt");                    
    fin >> layers;                                                          //
    int* size = new int[layers];                                            //
    for(int i = 0;i < layers;i++){                                          //          Descript Network
        fin >> size[i];                                                     //
    }                                                                       //
    fin.close();                                                            //

    size[0] += 2;

    double input[size[0]];

    int WomanIndex = size[0];
    int ManIndex = size[0]+1;   

    NeuronNetwork nn(layers,size);                                                                                      
    nn.LoadNetwork("..\\Network\\Network.txt");                  

    fin.open("..\\Telegram server\\bin\\Debug\\net7.0\\InOutUser\\input.txt");                                                  
    
    fin >> gender;
    input[WomanIndex] = (double)(gender == 'w');
    input[ManIndex]   = (double)(gender == 'm');

    if(gender == 'n'){
        if(rand()%2 == 0){
            input[WomanIndex] = 1.0;
        }else{
            input[ManIndex] = 1.0;
        }
    }

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

    double* SF = nn.SoftMax();
    double SFSum;
    std::pair<double,int> ans[size[layers-1]];

    for(int i = 0;i < size[layers-1];i++){
        ans[i] = {SF[i],i+1};
    }
    
    std::sort(ans,ans+size[layers-1]);
    std::reverse(ans,ans+size[layers-1]);

    for(int i = 0;i < 5;i++){
        SFSum += ans[i].first;
    }
    for(int i = 0;i < 5;i++){
        fout << ans[i].second << " " << std::fixed << std::setprecision(0) <<  (ans[i].first/SFSum*100.0) << '\n';
    }
    

    fout.close();                                                           

    return 0;
}
