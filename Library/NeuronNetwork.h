#pragma once
#include <thread>
#include "Neuron.h"
#include <iostream>
#include <iomanip>
#include <fstream>
#include <vector>

class NeuronNetwork {
public:

    int layers;
    int* size;
    Neuron** neurons;
    double*** weights;

    NeuronNetwork(int n, int* p) {

        layers = n;
        size = new int[n];
        neurons = new Neuron * [n];
        weights = new double** [n - 1];


        for (int i = 0; i < n; i++) {

            size[i] = p[i];
            neurons[i] = new Neuron[p[i]+1];

            neurons[i][p[i]].ActiveValue = 1.0;

            if(i == n-1)break;
            weights[i] = new double* [p[i]+1];
            for (int j = 0; j <= p[i]; j++) {
                weights[i][j] = new double[p[i + 1]];
            }

        }

    }
    ~NeuronNetwork() {
        delete[] size, neurons, weights;
    }

    void SetInput(double* p) {
        for (int i = 0; i < size[0]; i++) {
            neurons[0][i].ActiveValue = p[i];
        }
    }
    void SetRandom() {
        for (int i = 0; i < layers - 1; i++) {
            for (int j = 0; j < size[i]; j++) {
                for (int k = 0; k < size[i + 1]; k++) {
                    weights[i][j][k] = (rand() % 100) * 0.01;
                }
            }
        }
    }

    void ForwardFeed() {

        for (int i = 1; i < layers; i++) {
            for (int j = 0; j < size[i]; j++) {
                neurons[i][j].value = 0.0;
                for (int k = 0; k <= size[i - 1]; k++) {
                    neurons[i][j].value += neurons[i - 1][k].ActiveValue * weights[i - 1][k][j];
                }
                neurons[i][j].Activate();
            }
        }
            
    }
    int Predict() {

        double MaximalPercent;
        int MaxIndex;

        MaximalPercent = -1000;
        MaxIndex = 0;

        for (int i = 0; i < size[layers - 1]; i++) {
            if (MaximalPercent < neurons[layers - 1][i].ActiveValue) {
                MaximalPercent = neurons[layers - 1][i].ActiveValue;
                MaxIndex = i;
            }
        }

        return MaxIndex;

    }

    double ErrorCouter(double* ra) {

        double sum;
        sum = 0.0;

        for (int i = 0; i < size[layers - 1]; i++) {
            sum += pow((neurons[layers - 1][i].ActiveValue - ra[i]), 2);
        }

        return sum;

    }

    void Backward(int i, int start, int finish, double ls) {
        for (int j = start; j < finish; j++) {
            neurons[i][j].error = 0.0;
            for (int k = 0; k < size[i + 1]; k++) {
                neurons[i][j].error += (neurons[i + 1][k].error) * (neurons[i + 1][k].proiz()) * (weights[i][j][k]);
                weights[i][j][k] -= ls * (neurons[i + 1][k].error) * (neurons[i + 1][k].proiz()) * (neurons[i][j].ActiveValue);
            }
        }
    }

    void BackPropagation(double* ra, double ls) {

        

        for (int i = 0; i < size[layers - 1]; i++) {
            neurons[layers - 1][i].error = 2 * (neurons[layers - 1][i].ActiveValue - ra[i]);
        }

        for (int i = layers - 2; i >= 0; i--) {
            
            Backward(i,0,size[i]+1,ls);

            // std::thread th1([this, i, ls]() { Backward(i, 0, size[i]/4 , ls); });
            // std::thread th2([this, i, ls]() { Backward(i, size[i] / 4 + 1, size[i] / 2, ls); });
            // std::thread th3([this, i, ls]() { Backward(i, size[i] / 2 + 1, size[i] * 3 / 4, ls); });
            // std::thread th4([this, i, ls]() { Backward(i, size[i] * 3 / 4 + 1, size[i] , ls); });

            // th1.join();
            // th2.join();
            // th3.join();
            // th4.join();

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
        fout << '\n';
        for(int i = 0;i < layers-1;i++){
            for(int j = 0;j < size[i+1];j++){
                fout << weights[i][size[i]][j] << ' ';
            }
        }


        fout.close();
    }
    void LoadNetwork(char filename[]) {
        std::ifstream fin(filename);

        for (int i = 0; i < layers - 1; i++) {
            for (int j = 0; j < size[i]; j++) {
                for (int k = 0; k < size[i + 1]; k++) {
                    if (fin >> weights[i][j][k]) {
                        // Read a weight value from the file
                    }
                }
            }
        }

        for(int i = 0;i < layers-1;i++){
            for(int j = 0;j < size[i+1];j++){
                if(fin >> weights[i][size[i]][j] ){

                }
            }
        }

        fin.close();
    }

    double* SoftMax(){

        double SE = 0;
        double* ans = new double[size[layers-1]];

        for(int i = 0;i < size[layers-1];i++){
            ans[i] = exp(neurons[layers-1][i].value);
            SE += ans[i];
        }

        for(int i = 0;i < size[layers-1];i++){
            ans[i] = ans[i]/SE*100.0;
        }

        return ans;

    }

};
