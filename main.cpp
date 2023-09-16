#include <iostream>
#include <string>
#include <fstream>
#include <ctime>
#include <cmath>
#include <algorithm>
#include <iomanip>

using namespace std;

struct neuron {
    double value, error,bias,ActiveValue;

    double activfunc(double x){
        return 1.0/(1.0+exp(-x));
    }
    void AddBias(){
        value += bias;
    }
    void Activate(){
        ActiveValue = activfunc(value);
    }
    double proiz(){
        return activfunc(value)*(1-activfunc(value) );
    }
};

class network {
public:

    int layers;
    int *size;
    neuron **neurons;
    double ***weights;
    
    void SetLayers(int n, int *p) {

        layers = n;
        size = new int[n];
        neurons = new neuron *[n];
        weights = new double **[n - 1];
        

        for (int i = 0; i < n; i++) {

            size[i] = p[i];
            neurons[i] = new neuron[p[i]];

            if(i == n-1)continue;
            weights[i] = new double *[p[i]];
            for (int j = 0; j < p[i]; j++) {
                weights[i][j] = new double[p[i + 1]];
            }

        }

        ClearNeuronsValues();
        ClearNeuronsErrors();
        SetRandomBias();
        SetRandomWeights();
    }
    void SetInput(double *p) {
        for (int i = 0; i < size[0]; i++) {
            neurons[0][i].ActiveValue = p[i];
        }
    }
    
    void ClearNeuronsValues() {
        for (int i = 1; i < layers; i++) {
            for (int j = 0; j < size[i]; j++) {
                neurons[i][j].value = 0.0;
            }
        }
    }
    void ClearNeuronsErrors() {
        for (int i = 0; i < layers; i++) {
            for (int j = 0; j < size[i]; j++) {
                neurons[i][j].error = 0.0;
            }
        }
    }
    void SetRandomBias(){
        for(int i = 1;i < layers;i++){
            for(int j = 0;j < size[i];j++){
                neurons[i][j].bias = (rand()%100)*0.01;
            }
        }
    }
    void SetRandomWeights(){
        for(int i = 0;i < layers-1;i++){
            for(int j = 0;j < size[i];j++){
                for(int k = 0;k < size[i+1];k++){
                    weights[i][j][k] = (rand()%100)*0.01;
                }
            }
        }
    }

    void Forward() {

        ClearNeuronsValues();

        for(int i = 1;i < layers;i++){
            for(int j = 0;j < size[i];j++){
                for(int k = 0;k < size[i-1];k++){
                    neurons[i][j].value += neurons[i-1][k].ActiveValue*weights[i-1][k][j];
                }
                neurons[i][j].AddBias();
                neurons[i][j].Activate();
            }
        }

    }
    double ErrorCouter(double *ra){

        double sum;
        sum = 0.0;

        for(int i = 0;i < size[layers-1];i++){
            sum += pow((neurons[layers-1][i].ActiveValue-ra[i]),2);
        }

        return sum;

    }
    void BackPropogation(double *ra,double ls){
        
        ClearNeuronsErrors();

        for(int i = layers-1;i >= 0;i--){
            if(i == layers-1){
                for(int j = 0;j < size[i];j++){
                    neurons[i][j].error = 2.0*(neurons[layers-1][j].value - ra[j]);
                }
                continue;
            }

            
            for(int j = 0;j < size[i];j++){
                for(int k = 0;k < size[i+1];k++){
                    neurons[i][j].error +=    neurons[i+1][k].error * neurons[i+1][k].proiz() * weights[i][j][k]   ;
                    weights[i][j][k]    -= ls*neurons[i+1][k].error * neurons[i+1][k].proiz() * neurons[i][j].value ;
                    neurons[i][k].bias  -= ls*neurons[i+1][k].error * neurons[i+1][k].proiz();
                }
            }

        }


    }
    
    void OutputErrors(int lay){

        for(int i = 0;i < size[lay-1];i++){
            for(int j = 0;j < size[lay];j++){
                cout << fixed << setprecision(1) <<  weights[lay-1][i][j] << ' ';
            }
            //cout << '\n';
        }


        // for(int j = 0;j < size[lay];j++){
        //     cout << fixed << setprecision(1) << neurons[lay][j].error << ' ';
        // }
        cout << '\n';

    }
    void SaveWeights(string filename) {
            ofstream fout;
            fout.open(filename);
            for (int i = 0; i < layers - 1; i++) {
                for (int j = 0; j < size[i]; j++) {
                    for (int k = 0; k < size[i + 1]; k++) {
                        fout << weights[i][j][k] << ' ';
                    }
                }
            }
            fout.close();
        }
};


int main() {

    srand(time(nullptr));

    int n = 4, n_of_tests = 28, size[] = {4096, 64, 32, 26};

    char c;
    double mintst = 10000;


    network nn;//Neuron Network

    ifstream fin;

    double *rightans = new double [size[n-1]];

    nn.SetLayers(n, size);

    double *input;
    input = new double[size[0]];

    double s = 3.0;

    while (abs(s) > 0.1){
        
        fin.open("assets\\lib.txt");

        for (int i = 0; i < n_of_tests; i++) {
            for (int j = 0; j < size[0]; j++) {
                fin >> input[j];
            }

            fin >> c;
            nn.SetInput(input);

            nn.ClearNeuronsValues();
            nn.Forward();



            fill(rightans,rightans+size[n-1],0.0);
            rightans[c-'a'] = 1.0;



            nn.BackPropogation(rightans,1);

            if (i == 0) {
                nn.OutputErrors(4);
                cout << '\n';
            }

            s = nn.ErrorCouter(rightans);

            if (s < mintst) {
                mintst = s;
                nn.SaveWeights("assets\\perfect_weights.txt");
            }

        }

        cout << mintst << '\n';
        fin.close();

    }

    return 0;
}
