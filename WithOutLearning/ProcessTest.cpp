#include "..\Library\NeuronNetwork.h"
#include <fstream>

int main(){

   std::ifstream fin;

   char UserId[256];                                                        
   int n,c,layers;                                                                


    /*Set network size*/                                   
   fin.open("..\\NetworkDescription\\NetworkSize.txt");                    
   fin >> layers;                                                          
   int* size = new int[layers];                                            
   for(int i = 0;i < layers;i++){                                          
       fin >> size[i];                                                     
   }                                                                       
   fin.close();                                                            

   double input[size[0]];   


   /*Set network weights*/
   NeuronNetwork nn(layers,size);                                                                                    
   nn.LoadNetwork("..\\NetworkDescription\\Network.txt");                  

   fin.open("..\\Telegram server\\bin\\Debug\\net7.0\\InOutUser\\input.txt");                                                  
   
   std::fill(input,input+size[0],0.0);

    /*Input question*/

   //fin >> UserId;                                                          
   fin >> n;                                                               
   for(int i = 0;i < n;i++){                                               
       fin >> c;                                                           
       input[c-1] = 1.0;                                                   
   }                                                                       
   fin.close();                                                            
   nn.SetInput(input);

    /*Answer question*/
   nn.ForwardFeed();                                                       
   std::ofstream fout("..\\Telegram server\\bin\\Debug\\net7.0\\InOutUser\\output.txt");                                       
   fout << nn.Predict()+1;       
   fout.close();                                                           

   return 0;
}
