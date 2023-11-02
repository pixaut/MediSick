#include <iostream>
#include <cstdio>
#include <fstream>
#include <algorithm>
#include <cmath>
#include <vector>
 
using namespace std;
 
vector<long long> t;
long long ans,n,N;
 
 
long long getmax(int l,int r){
    ans = INT_MIN;
 
    l += N;r += N;
 
    while(l <= r){
 
        if(l%2 == 1) ans = max(ans,t[l++]);
        if(r%2 == 0) ans = max(ans,t[r--]);
        l /= 2;r /= 2;
    }
 
    return ans;
 
}
 
int main()
{
 
    freopen("mushrooms.in","r",stdin);
    freopen("mushrooms.out","w",stdout);
 
    long long o,x1,y1,m,x,y;
    long long k = 0;
 
    cin >> n;
 
    t = vector<long long>(2*N,INT_MIN);
    
    N = 1;
    while(N < n)N *= 2;

    for(int i = 0;i < n;i++){
        cin >> t[N+i];
    }
 
    for(int i = N-1;i > 01;i--){
        t[i] = max(t[i*2],t[i*2+1]);
    }
 
 
    cin >> m >> x >> y;
 
    for(int i = 0;i < m;i++){
        o = getmax(x,y);
        k += o;
        x1 = x;y1 = y;
        x = min(y1,(x1*o + o*o)%n);
        y = max(y1,(x1*o + o*o)%n);
 
    }
 
    cout << k;
 
    return 0;
}