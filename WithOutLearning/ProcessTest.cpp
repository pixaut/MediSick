#include "..\Library\NeuronNetwork.h"
#include <fstream>
#include <utility>
#include <algorithm>

int main(){

    std::ifstream fin;                         
    int n,c,layers;                                                  
    char gender;
    const char *InOut = "..\\Telegram server\\bin\\Debug\\net7.0\\InOutUser\\";
    const char *NetworkPath = "..\\Network\\";


    fin.open(NetworkPath + "NetworkSize.txt");                    
    fin >> layers;                                                          
    int* size = new int[layers];                                            
    for(int i = 0;i < layers;i++){                                                  
        fin >> size[i];                                                     
    }                                                                       
    fin.close();                                                            



    size[0] += 2;

    double input[size[0]];
    int WomanIndex = size[0] , ManIndex = size[0]+1, NeutralEl;
    NeuronNetwork nn(layers,size);                                                                                      
    nn.LoadNetwork(NetworkPath + "Network.txt");                  
    fin.open(InOut + "input.txt");                                                  
    


    fin >> gender;
    
    if(gender == 'n'){
        if(rand()%2) gender = 'w';
        else gender = 'm';
    }

    if(gender == 'm') input[ManIndex] = 1.0;
    else input[WomanIndex] = 1.0;
    




    std::fill(input,input+size[0],0.0);
    fin >> n;                                                               
    for(int i = 0;i < n;i++){                                               
        fin >> c;                                                           
        input[c-1] = 1.0;                                                   
    }                                                                       
    fin.close();                                                            
    nn.SetInput(input);




    nn.ForwardFeed();                                                       
    std::ofstream fout(InOut + "output.txt");  

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
