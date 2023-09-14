#include <iostream>
#include <string>
#include <fstream>
#include <ctime>
#include <cmath>
#include <algorithm>
#include <iomanip>
// jkhkhkgkhk4454jhkhkjdh
using namespace std;

struct neuron
{
    double value, error;
};

class network
{
public:
    int layers;
    int *size;
    double *z;
    neuron **neurons;
    double ***weights;

    double activation(double x)
    {
        return (exp(x) - exp(-x)) / (exp(x) + exp(-x));
    }
    double activation_proizv(double x)
    {
        return 1 - pow(activation(x), 2);
    }
    void SetLayers(int n, int *p)
    {

        srand(time(nullptr));

        layers = n;
        size = new int[n];
        neurons = new neuron *[n];
        z = new double[p[n - 1]];
        weights = new double **[n - 1];

        for (int i = 0; i < size[p[n - 1]]; i++)
        {
            z[i] = 0.0;
        }

        for (int i = 0; i < n; i++)
        {
            size[i] = p[i];
            neurons[i] = new neuron[p[i]];

            if (i == n - 1)
                continue;

            weights[i] = new double *[p[i]];
            for (int j = 0; j < p[i]; j++)
            {
                weights[i][j] = new double[p[i + 1]];
                for (int k = 0; k < p[i + 1]; k++)
                {
                    weights[i][j][k] = ((rand() % 100) * 0.01);
                }
            }
        }
    }
    void SetInput(double *p)
    {
        for (int i = 0; i < size[0]; i++)
        {
            neurons[0][i].value = p[i];
        }
    }
    void SaveWeights(string filename)
    {
        ofstream fout;
        fout.open(filename);
        for (int i = 0; i < layers - 1; i++)
        {
            for (int j = 0; j < size[i]; j++)
            {
                for (int k = 0; k < size[i + 1]; k++)
                {
                    fout << weights[i][j][k] << ' ';
                }
            }
        }
        fout.close();
    }
    void ClearNeuronsValues()
    {
        for (int i = 1; i < layers; i++)
        {
            for (int j = 0; j < size[i]; j++)
            {
                neurons[i][j].value = 0.0;
            }
        }
    }
    void ClearNeuronsErrors()
    {
        for (int i = 0; i < layers; i++)
        {
            for (int j = 0; j < size[i]; j++)
            {
                neurons[i][j].error = 0.0;
            }
        }
    }
    void Forward()
    {
        for (int i = 0; i < layers - 2; i++)
        {
            for (int k = 0; k < size[i + 1]; k++)
            {
                for (int j = 0; j < size[i]; j++)
                {
                    neurons[i + 1][k].value += neurons[i][j].value * weights[i][j][k];
                }
                if (i == layers - 3)
                    continue;
                neurons[i + 1][k].value = activation(neurons[i + 1][k].value);
            }
        }
    }
    void Softmax()
    {

        double sum;
        sum = 0.0;

        for (int i = 0; i < size[layers - 1]; i++)
        {
            sum += exp(neurons[layers - 1][i].value);
        }

        for (int i = 0; i < size[layers - 1]; i++)
        {
            z[i] = exp(neurons[layers - 1][i].value) / sum;
        }
    }
    double ErrorCouter(double *ra)
    {

        double sum;
        sum = 0.0;

        for (int i = 0; i < size[layers - 1]; i++)
        {
            sum += ra[i] * log(z[i]);
        }

        return sum;
    }
    void BackPropogation(double *ra, double ls)
    {

        for (int i = layers - 1; i >= 0; i--)
        {
            if (i == layers - 1)
            {
                for (int j = 0; j < size[i]; j++)
                {
                    neurons[i][j].error = z[j] - ra[j];
                }
                continue;
            }

            for (int j = 0; j < size[i]; j++)
            {
                for (int k = 0; k < size[i + 1]; k++)
                {
                    neurons[i][j].error += neurons[i + 1][k].error * weights[i][j][k];
                    weights[i][j][k] -= ls * neurons[i + 1][k].error * neurons[i][j].value; // bb
                }
                neurons[i][j].error = activation_proizv(neurons[i][j].error);
            }
        }
    }
    void OutputErrors(int lay)
    {

        /*for(int i = 0;i < size[lay-1];i++){
            for(int j = 0;j < size[lay];j++){
                cout << fixed << setprecision(1) <<  weights[lay-1][i][j] << ' ';
            }
            //cout << '\n';
        }*/

        for (int j = 0; j < size[lay]; j++)
        {
            cout << fixed << setprecision(1) << neurons[lay][j].error << ' ';
        }
        cout << '\n';
    }
};

int main()
{

    ios_base::sync_with_stdio(false);
    cout.tie();

    int n = 4, n_of_tests = 28, size[] = {4096, 64, 32, 26};

    char c;
    double mintst = 10000;

    network nn; // Neuron Network

    ifstream fin;

    double *rightans = new double[size[n - 1]];

    nn.SetLayers(n, size);

    double *input;
    input = new double[size[0]];

    double s = 3.0;

    while (abs(s) > 0.1)
    {

        fin.open("assets\\lib.txt");

        for (int i = 0; i < n_of_tests; i++)
        {
            for (int j = 0; j < size[0]; j++)
            {
                fin >> input[j];
            }

            fin >> c;
            nn.SetInput(input);

            nn.ClearNeuronsValues();
            nn.Forward();

            fill(rightans, rightans + size[n - 1], 0.0);
            rightans[c - 'a'] = 1.0;

            nn.Softmax();
            nn.BackPropogation(rightans, 0.1);

            if (i == 0)
            {
                nn.OutputErrors(2);
                cout << '\n';
            }

            s = nn.ErrorCouter(rightans);
            nn.ClearNeuronsErrors();
            /*for(int j = 0;j < size[n-1];j++){
                cout << fixed << setprecision(9)<< rightans[j] << ' ';
            }
            cout << '\n';
            for(int j = 0;j < size[n-1];j++){
                cout << fixed << setprecision(9) << nn.neurons[n-1][j].value << ' ';
            }
            cout << '\n';*/

            if (s < mintst)
            {
                mintst = s;
                nn.SaveWeights("assets\\perfect_weights.txt");
            }
        }

        cout << mintst << '\n';
        fin.close();
    }

    return 0;
}
