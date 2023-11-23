#include "..\Library\NeuronNetwork.h"
#include <fstream>
#include <utility>
#include <algorithm>

/* programm that process test */


int main(){

    std::ifstream fin;                         
    int n,c,layers;                                                  
    char gender;
    const char *InOut = "..\\Telegram server\\bin\\Debug\\net7.0\\InOutUser\\"; // path to folder that consist input and output files
    const char *NetworkPath = "..\\Network\\"; // path to network descrription

    // getting network 

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

    // setting and filling network 

    NeuronNetwork nn(layers,size);                                                                                      
    nn.LoadNetwork(NetworkPath + "Network.txt");                  
    fin.open(InOut + "input.txt");                                                  
    


    fin >> gender;
    
    if(gender == 'n'){ // none gender exeption
        if(rand()%2) gender = 'w';
        else gender = 'm';
    }

    if(gender == 'm') input[ManIndex] = 1.0; // setting gender flag
    else input[WomanIndex] = 1.0;
    


    // getting input

    std::fill(input,input+size[0],0.0);
    fin >> n;                                                               
    for(int i = 0;i < n;i++){                                               
        fin >> c;                                                           
        input[c-1] = 1.0;                                                   
    }                                                                       
    fin.close();             

    //setting input                                               
    nn.SetInput(input);

    // calculating result
    nn.ForwardFeed();                


    //output probability of sicks

    std::ofstream fout(InOut + "output.txt");  

    const double* SF = nn.SoftMax(); 
    double SFSum;
    std::pair<double,int> ans[size[layers-1]];

    for(int i = 0;i < size[layers-1];i++){
        ans[i] = {SF[i],i+1};
    }
    
    std::sort(ans,ans+size[layers-1]); // sorting more probable sicks
    std::reverse(ans,ans+size[layers-1]);

    for(int i = 0;i < 5;i++){
        SFSum += ans[i].first;
    }
    for(int i = 0;i < 5;i++){
        fout << ans[i].second << " " << std::fixed << std::setprecision(0) <<  (ans[i].first/SFSum*100.0) << '\n'; // calculating relative probability
    }

    fout.close();                                                           

    return 0;
}
