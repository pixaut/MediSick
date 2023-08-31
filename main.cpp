#include <bits/stdc++.h>

using namespace std;

struct neuron {

    double value, error;

    void activate() {
        value = (1 / (1 + pow(2.71828, -value))); // e = 2.718281828
    }

};

class network {
public:

    int layers;
    int *size;
    double *percents;
    neuron **neurons;
    double ***weights;

    void SetLayers(int n, int *p) {
        layers = n;
        size = new int[n];
        neurons = new neuron *[n];
        percents = new double [p[n-1]];
        weights = new double **[n - 1];


        for(int i = 0;i < size[p[n-1]];i++){
            percents[i] = 0.0;
        }

        for (int i = 0; i < n; i++) {
            size[i] = p[i];
            neurons[i] = new neuron[p[i]];

            if (i == n - 1) continue;

            weights[i] = new double *[p[i]];
            for (int j = 0; j < p[i]; j++) {
                weights[i][j] = new double[p[i + 1]];
                for (int k = 0; k < p[i + 1]; k++) {
                    weights[i][j][k] = ((rand() % 100) * 0.01);// maybe not work
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
    void WeightsUpdater() {
        for (int i = 0; i < layers - 1; i++) {
            for (int j = 0; j < size[i]; j++) {
                for (int k = 0; k < size[i + 1]; k++) {
                    weights[i][j][k] = (double(rand() % 100) * 0.01) ;
                }
            }
        }
    }
    int  predict() {
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
    }
    void Forward() {
        double value;
        for (int i = 0; i < layers - 2; i++) {

            for (int k = 0; k < size[i + 1]; k++) {
                value = 0.0;
                for (int j = 0; j < size[i]; j++) {
                    value += neurons[i][j].value * weights[i][j][k];
                }
                neurons[i + 1][k].value = value;
                if(i == layers-3)continue;
                neurons[i + 1][k].activate();
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
            percents[i] = exp(neurons[layers-1][i].value)/sum;
        }

    }
    double ErrorCouter(double *ra){

        Softmax();

        double sum;
        sum = 0.0;

        for(int i = 0;i < size[layers-1];i++){
            sum += ra[i]*log(percents[i]);
        }

        return -sum;

    }

};


int main() {

    srand(time(nullptr));
    int n = 4, n_of_tests = 28, size[] = {4096, 64, 32, 26};

    char c;
    double mintst = 0;


    network nn;
    ifstream fin;

    double *rightans = new double [size[n-1]];

    nn.SetLayers(n, size);

    auto *input = new double[size[0]];

    double s = 3.0;

    while (s > 1.0){

         for(int i = 0;i < size[n-1];i++){
             rightans[i] = 0.0;
         }

        nn.WeightsUpdater();

        fin.open("assets\\lib.txt");

        for (int i = 0; i < n_of_tests; i++) {
            for (int j = 0; j < size[0]; j++) {
                fin >> input[j];
            }

            fin >> c;
            nn.SetInput(input);
            nn.Forward();

            rightans[c-'a'] = 1.0;

            s = nn.ErrorCouter(rightans);

            if (s < mintst) {
                mintst = s;
                nn.SaveWeights("assets\\perfect_weights.txt");
            }

        }

        cout << s << '\n';
        fin.close();

    }

    return 0;
}

