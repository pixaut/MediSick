import cv2
import os.path

f = open('lib.txt', 'w')

for k in range(25):
    path = 'assets\\images\\'+'letter'+str(k)+'.png'
    img = cv2.imread(path, cv2.IMREAD_GRAYSCALE)
    w = img.shape[0]
    h = img.shape[1]

    for i in range(w):
        for j in range(h):
            a = img[i, j]
            ans = round((255 - a) / 255, 1)
            f.write(str(ans))
            f.write(' ')
        f.write('\n')
    f.write('\n')
f.close()


