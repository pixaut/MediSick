#include <bits/stdc++.h>

using namespace std;

struct neuron {
    double value, error;
};

class network {
public:

    int layers;
    int *size;
    double *z;
    neuron **neurons;
    double ***weights;

    double activation(double x){
        //if(x >  35.0) return 1.0;
        //if(x < -35.0) return 0.0;
        //return 1.0/(1.0+exp(-x));
        return max(0.0,x);
    }
    double activation_proizv(double x){
        //if(abs(x) > 15.0) return 0.0;
        //return activation(x)*(1.0-activation(x));
        if(x > 0) return 1.0;
        else return 0.0;
    }
    void SetLayers(int n, int *p) {

        srand(time(nullptr));

        layers = n;
        size = new int[n];
        neurons = new neuron *[n];
        z = new double [p[n-1]];
        weights = new double **[n - 1];


        for(int i = 0;i < size[p[n-1]];i++){
            z[i] = 0.0;
        }

        for (int i = 0; i < n; i++) {
            size[i] = p[i];
            neurons[i] = new neuron[p[i]];

            if (i == n - 1) continue;

            weights[i] = new double *[p[i]];
            for (int j = 0; j < p[i]; j++) {
                weights[i][j] = new double[p[i + 1]];
                for (int k = 0; k < p[i + 1]; k++) {
                    weights[i][j][k] = ((rand() % 100) * 0.01);
                }
            }
        }


    }
    void SetInput(double *p) {
        for (int i = 0; i < size[0]; i++) {
            neurons[0][i].value = p[i];
        }
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
    /*int  predict() {
        int imax;
        double max;
        imax = 0;
        max = 0.0;
        for (int i = 0; i < size[layers - 1]; i++) {
            if (percents[i] > max) {
                max = percents[i];
                imax = i;
            }
        }
        return imax;
    }*/
    void Forward() {
        double value;
        for (int i = 0; i < layers - 2; i++) {
            for (int k = 0; k < size[i + 1]; k++) {
                value = 0.0;
                for (int j = 0; j < size[i]; j++) {
                    value += neurons[i][j].value * weights[i][j][k];
                }
                if(i == layers-3)neurons[i + 1][k].value = value;
                else neurons[i + 1][k].value = activation(value);

            }
        }
    }
    void Softmax(){

        double sum;
        sum = 0.0;

        for(int i = 0;i < size[layers-1];i++){
            sum += exp(neurons[layers-1][i].value);
        }

        for(int i = 0;i < size[layers-1];i++){
            z[i] = exp(neurons[layers-1][i].value)/sum;
        }

    }
    double ErrorCouter(double *ra){

        double sum;
        sum = 0.0;

        for(int i = 0;i < size[layers-1];i++){
            sum += abs(z[i]-ra[i]);
        }

        return sum;

    }
    void BackPropogation(double *ra,double ls){

        double sum;
        sum = 0.0;

        for(int i = layers-1;i >= 0;i--){
            if(i == layers-1){
                for(int j = 0;j < size[i];j++){
                    neurons[i][j].error = z[j] - ra[j];
                }
                continue;
            }

            for(int j = 0;j < size[i];j++){
                sum = 0.0;
                for(int k = 0;k < size[i+1];k++){
                    sum += neurons[i+1][k].error * weights[i][j][k];
                    weights[i][j][k] -= ls*neurons[i+1][k].error * neurons[i][j].value;//bb
                }
                neurons[i][j].error = activation_proizv(sum);
            }

        }


    }
    void OutputErrors(int lay){

        for(int j = 0;j < size[lay];j++){
            cout << fixed << setprecision(1) << neurons[lay][j].value << ' ';
        }
        cout << '\n';

    }

};


int main() {


    int n = 4, n_of_tests = 28, size[] = {4096, 64, 32, 26};

    char c;
    double mintst = 10000;


    network nn;//NeuronNetwork

    ifstream fin;

    double *rightans = new double [size[n-1]];

    nn.SetLayers(n, size);

    auto *input = new double[size[0]];

    double s = 3.0;

    while (s > 1.0){

        fin.open("assets\\lib.txt");

        for (int i = 0; i < n_of_tests; i++) {
            for (int j = 0; j < size[0]; j++) {
                fin >> input[j];
            }

            fin >> c;
            nn.SetInput(input);
            nn.Forward();



            fill(rightans,rightans+size[n-1],0.0);
            rightans[c-'a'] = 1.0;

            nn.Softmax();
            nn.BackPropogation(rightans,0.001);
            /*for(int j = 0;j < size[n-1];j++){
                cout << fixed << setprecision(9)<< rightans[j] << ' ';
            }
            cout << '\n';
            for(int j = 0;j < size[n-1];j++){
                cout << fixed << setprecision(9) << nn.neurons[n-1][j].value << ' ';
            }
            cout << '\n';*/
            if(i == 0){
                nn.OutputErrors(1);
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

