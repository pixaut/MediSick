#include <bits/stdc++.h>

using namespace std;

struct neuron{

    double value,error;

    void activate(){
        value = (1/(1 + pow(2.71828,-value) ) ); // e = 2.718281828
    }

};

class network{
public:

    int layers;
    int* size;
    neuron** neurons;
    double*** weights;

    void SetLayers(int n,int* p){
        layers = n;
        size = new int[n];
        neurons = new neuron* [n];
        weights = new double** [n-1];

        for(int i = 0;i < n;i++){
            size[i] = p[i];
            neurons[i] = new neuron[p[i]];

            if(i == n-1) continue;

            weights[i] = new double* [p[i]];
            for(int j = 0;j < p[i];j++){
                weights[i][j] = new double[p[i+1]];
                for(int k = 0;k < p[i+1];k++){
                    weights[i][j][k] = ((rand()%100)*0.01) / p[i];// maybe not work
                }
            }
        }


    }
    void SetInput(double* p){
        for(int i = 0;i < size[0];i++){
            neurons[0][i].value = p[i];
        }
    }
    void SaveWeights(string filename){
        ofstream fout;
        fout.open(filename);
        for(int i = 0;i < layers-1;i++){
            for(int j = 0;j < size[i];j++){
                for(int k = 0;k < size[i+1];k++){
                    fout << weights[i][j][k] << ' ';
                }
            }
        }
        fout.close();
    }
    void WeightsUpdater(){
        for(int i = 0; i < layers-1;i++){
            for(int j = 0;j < size[i];j++){
                for(int k = 0;k < size[i+1];k++){
                    weights[i][j][k] = (double(rand()%100)*0.01) / double(size[i]);
                }
            }
        }
    }
    int  predict(){
        int imax;
        double max;
        imax = 0;
        max = 0.0;
        for(int i = 0;i < size[layers-1];i++){
            if(neurons[layers-1][i].value > max){
                max = neurons[layers-1][i].value;
                imax = i;
            }
        }
        return imax;
    }
    void Forward(){
        double value;
        for(int i = 0;i < layers-1;i++){
            for(int k = 0;k < size[i+1];k++){
                value = 0.0;
                for(int j = 0;j < size[i];j++){
                    value += neurons[i][j].value * weights[i][j][k];
                }
                neurons[i+1][k].value = value;
                neurons[i+1][k].activate();
            }
        }
    }

};



int main(){

    srand(time(nullptr));
    int n = 4,n_of_tests = 28,size[] = {4096,64,32,26};

    char c;
    int ra,ranswer,maxtst = 0;//right answers
    network nn;
    ifstream fin;

    nn.SetLayers(n,size);

    auto* input = new double[size[0]];

    while(double(ra)/double(n_of_tests) < 0.6){

        nn.WeightsUpdater();

        fin.open("assets\\lib.txt");

        ra = 0;
        for(int i = 0;i < n_of_tests;i++){
            for(int j = 0;j < size[0];j++){
                fin >> input[j];
            }

            fin >> c;
            nn.SetInput(input);
            nn.Forward();
            ranswer = c-'a';
            if(nn.predict() == ranswer) ra++;
        }

        cout << double(ra)/double(n_of_tests)*100.0 << '\n';

        fin.close();

        if(ra > maxtst){
            maxtst = ra;
            nn.SaveWeights("assets\\perfect_weights.txt");
        }
    }

    return 0;
}

