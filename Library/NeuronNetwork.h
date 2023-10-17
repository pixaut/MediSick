#include "Neuron.h"
#include <iostream>
#include <iomanip>
#include <fstream>

class NeuronNetwork {
public:

    int layers;
    int *size;
    Neuron **neurons;
    double ***weights;
    
    NeuronNetwork(int n, int *p) {

        layers = n;
        size = new int[n];
        neurons = new Neuron *[n];
        weights = new double **[n - 1];
        

        for (int i = 0; i < n; i++) {

            size[i] = p[i];
            neurons[i] = new Neuron[p[i]];

            if(i == n-1)break;
            weights[i] = new double *[p[i]];
            for (int j = 0; j < p[i]; j++) {
                weights[i][j] = new double[p[i + 1]];
            }

        }

    }
    
    void SetInput(double *p) {
        for (int i = 0; i < size[0]; i++) {
            neurons[0][i].ActiveValue = p[i];
        }
    }
    void SetRandom(){
        for(int i = 0;i < layers-1;i++){
            for(int j = 0;j < size[i];j++){
                for(int k = 0;k < size[i+1];k++){
                    weights[i][j][k] = (rand()%100)*0.01;
                }
            }
        }
        for(int i = 1;i < layers;i++){
            for(int j = 0;j < size[i];j++){
                neurons[i][j].bias = (rand()%100)*0.01;
            }
        }
    }

    void ForwardFeed() {

        for(int i = 1;i < layers;i++){
            for(int j = 0;j < size[i];j++){
                neurons[i][j].value = 0.0;
                for(int k = 0;k < size[i-1];k++){
                    neurons[i][j].value += neurons[i-1][k].ActiveValue*weights[i-1][k][j];
                }
                neurons[i][j].value +=  neurons[i][j].bias;
                neurons[i][j].Activate();
            }
        }

    }
    int Predict(){
        
        double MaximalPercent;
        int MaxIndex;

        MaximalPercent = -1000;
        MaxIndex = 0;

        for(int i = 0;i < size[layers-1];i++){
            if(MaximalPercent < neurons[layers-1][i].ActiveValue){
                MaximalPercent = neurons[layers-1][i].ActiveValue;
                MaxIndex = i;
            }
        }

        return MaxIndex;

    }
    
    double ErrorCouter(double *ra){

        double sum;
        sum = 0.0;

        for(int i = 0;i < size[layers-1];i++){
            sum += pow((neurons[layers-1][i].ActiveValue-ra[i]),2);
        }

        return sum;

    }
    void BackPropogation(double *ra,double ls){//add threads

        for(int i = 0;i < size[layers-1];i++){
            neurons[layers-1][i].error = 2*(neurons[layers-1][i].ActiveValue - ra[i]);
        }

        for(int i = layers-2;i >= 0;i--){
            for(int j = 0;j < size[i];j++){
                neurons[i][j].error = 0.0;
                for(int k = 0;k < size[i+1];k++){
                    neurons[i][j].error +=      (neurons[i+1][k].error) * (neurons[i+1][k].proiz()) * (weights[i][j][k])   ;
                    weights[i][j][k]    -= ls * (neurons[i+1][k].error) * (neurons[i+1][k].proiz()) * (neurons[i][j].ActiveValue) ;
                    neurons[i][k].bias  -= ls * (neurons[i+1][k].error) * (neurons[i+1][k].proiz());
                }
            }

        }


    }
    
    void SaveNetwork(char filename[]) {

            std::ofstream fout(filename);
            for (int i = 0; i < layers - 1; i++) {
                for (int j = 0; j < size[i]; j++) {
                    for (int k = 0; k < size[i + 1]; k++) {
                        fout << std::fixed << std::setprecision(8) << weights[i][j][k] << ' ';
                    }
                    fout << '\n';
                }
            }

            for(int i = 1;i < layers;i++){
                for(int j = 0;j < size[j];j++){
                    fout << neurons[i][j].bias << ' ';
                }
            }


            fout.close();
    }
    void LoadNetwork(char *filename){

        std::ifstream fin(filename);

        for (int i = 0; i < layers - 1; i++) {
            for (int j = 0; j < size[i]; j++) {
                for (int k = 0; k < size[i + 1]; k++) {
                    fin >> weights[i][j][k];
                }
            }
        }

        for(int i = 1;i < layers;i++){
            for(int j = 0;j < size[j];j++){
                fin >> neurons[i][j].bias;
            }
        }
        fin.close();

    }

};