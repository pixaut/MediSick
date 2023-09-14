import pygame
import math
import pathlib
from pathlib import Path
import cv2
pygame.init()
window = pygame.display.set_mode((900, 900))
pygame.display.set_caption("Paint/ai v. 1.5")
clock = pygame.time.Clock()

WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
GRAY = (156, 156, 156)
GRAYD = (65, 65, 65)
RED = (210, 30, 30)
GREEN = (77, 209, 29)
BLUE = (20, 179, 228)
PURPLE = (204, 204, 255)
PURPLE2 = (204, 204, 255)
PURPLE3 = (204, 204, 255)
PURPLE4 = (204, 204, 255)


rect1 = pygame.Rect((80, 780, 300, 100))
rect2 = pygame.Rect((500, 780, 300, 100))
rect3 = pygame.Rect((120, 50, 640, 640))
rect4 = pygame.Rect((800, 50, 50, 50))
rect5 = pygame.Rect((800, 100, 50, 650))
rect7 = pygame.Rect((800, 50, 50, 650))
rect8 = pygame.Rect((120, 50, 640, 640))
rect9 = pygame.Rect((85, 785, 290, 90))
rect10 = pygame.Rect((505, 785, 290, 90))


pygame.font.init()
font1 = pygame.font.SysFont("Bahnschrift", 65)
font4 = pygame.font.SysFont("Bahnschrift", 60)
font2 = pygame.font.SysFont("Arial", 40)
font3 = pygame.font.SysFont("Arial", 25)

savefilepic = font1.render("SAVE", False, (WHITE))
clearpic = font1.render("CLEAR", False, (WHITE))

savefilepic2 = font4.render("SAVE", False, (WHITE))
clearpic2 = font4.render("CLEAR", False, (WHITE))

letterpic = font2.render("L", False, (WHITE))
letters = pygame.image.load(r"ls.png")
video = cv2.VideoCapture("scrsar.avi")
success, video_image = video.read()
fps = video.get(cv2.CAP_PROP_FPS)


app = True
matrix = [[0 for j in range(65)] for i in range(65)]
letterset = [
    "a",
    "b",
    "c",
    "d",
    "e",
    "f",
    "g",
    "h",
    "i",
    "j",
    "k",
    "l",
    "m",
    "n",
    "o",
    "p",
    "q",
    "r",
    "s",
    "t",
    "u",
    "v",
    "w",
    "x",
    "y",
    "z",
]
letter = False
flag = False
flag2 = False
flag3 = False
flag4 = False
screensaver = True
letternum = 1
fn = str()


while screensaver:
    clock.tick(fps)
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            screensaver = False

    success, video_image = video.read()
    if success:
        video_surf = pygame.image.frombuffer(
            video_image.tobytes(), video_image.shape[1::-1], "BGR"
        )
    else:
        screensaver = False
    window.blit(video_surf, (0, 0))
    pygame.display.flip()


def drawGrid():
    blockSize = 10
    for x in range(120, 760, blockSize):
        for y in range(50, 690, blockSize):
            rect = pygame.Rect(x, y, blockSize, blockSize)
            pygame.draw.rect(window, GRAY, rect, 1)


def drawsquare(x, y):
    rect = pygame.Rect((x * 10), (y * 10), 10, 10)
    pygame.draw.rect(window, BLACK, rect, 5)


def drawgui():
    window.fill(PURPLE2)
    pygame.draw.rect(window, PURPLE, rect8, 2000)
    drawGrid()
    pygame.draw.rect(window, BLACK, rect3, 2)
    pygame.draw.rect(window, BLACK, rect4, 50)
    pygame.draw.rect(window, GRAY, rect4, 2)
    window.blit(letterpic, (815, 50))


def drawletgui():
    h = 0
    window.blit(letters, (800, 100))
    pygame.draw.rect(window, BLACK, rect4, 50)
    pygame.draw.rect(window, GRAY, rect7, 2)
    window.blit(letterpic, (815, 50))


def clear():
    pygame.draw.rect(window, PURPLE, rect8, 2000)
    drawGrid()
    pygame.draw.rect(window, BLACK, rect3, 2)

    for i in range(65):
        for j in range(65):
            matrix[i][j] = 0


def save():
    folder = Path("letterspictures", letterset[letternum - 1])
    if folder.is_dir():
        folder_c = len([1 for file in folder.iterdir()])
    filename = Path(
        "letterspictures",
        letterset[letternum - 1],
        letterset[letternum - 1] + str(folder_c + 1) + ".txt",
    )
    f = open(filename, "w")
    for i in range(64):
        for j in range(64):
            f.write(str(matrix[i][j]))
            f.write(" ")
        f.write("\n")
    f.close()


drawgui()
while app:
    clock.tick(560)
    pressed = pygame.mouse.get_pressed()
    pos = pygame.mouse.get_pos()
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            app = False

    if (pos[0] > 120 and pos[0] < 755) and (pos[1] > 50 and pos[1] < 685):
        if pressed[0]:
            matrix[round((pos[1] - 50) / 10)][round((pos[0] - 120) / 10)] = 1
            # print(round((pos[0] - 120) / 10), " ", round((pos[1] - 50) / 10))
            drawsquare(round((pos[0]) / 10), round((pos[1]) / 10))

    if (
        pressed[0]
        and flag2 == False
        and (pos[0] > 80 and pos[0] < 380)
        and (pos[1] > 780 and pos[1] < 880)
    ):
        save()
        flag2 = True
    if pressed[0] == False:
        flag2 = False

    if (
        (pressed[0])
        and (pos[0] > 500 and pos[0] < 800)
        and (pos[1] > 780 and pos[1] < 880)
    ):
        clear()

    if (pos[0] > 80 and pos[0] < 380) and (pos[1] > 780 and pos[1] < 880):
        pygame.draw.rect(window, PURPLE, rect1, 20000)
        pygame.draw.rect(window, BLACK, rect9, 200)
        window.blit(savefilepic2, (155, 802))
    else:
        pygame.draw.rect(window, BLACK, rect1, 200)
        window.blit(savefilepic, (150, 800))

    if (pos[0] > 500 and pos[0] < 800) and (pos[1] > 780 and pos[1] < 880):
        pygame.draw.rect(window, PURPLE, rect2, 20000)
        pygame.draw.rect(window, BLACK, rect10, 200)
        window.blit(clearpic2, (555, 802))
    else:
        pygame.draw.rect(window, BLACK, rect2, 200)
        window.blit(clearpic, (550, 800))

    if (
        (pressed[0] and letter == False)
        and (pos[0] > 800 and pos[0] < 850)
        and (pos[1] > 50 and pos[1] < 100)
    ):
        if flag == False:
            letter = True
            drawletgui()
            flag = True

    if pressed[0] == False:
        flag = False

    if (
        (pressed[0] and letter == True and flag == False)
        and (pos[0] > 800 and pos[0] < 850)
        and (pos[1] > 50 and pos[1] < 100)
    ):
        pygame.draw.rect(window, PURPLE, rect5, 500)
        pygame.draw.rect(window, BLACK, rect4, 50)
        window.blit(letterpic, (815, 50))
        letter = False
        flag = True

    if (
        (pressed[0] and letter == True)
        and (pos[0] > 800 and pos[0] < 850)
        and (pos[1] > 100 and pos[1] < 700)
    ):
        letternum = math.floor(((pos[1] - 100) / 23.07692307692) + 1)
        # print(letterset[letternum - 1], "    ", letternum - 1)
        drawletgui()
        rect6 = pygame.Rect((800, (letternum * 23.07692307692) + 75, 50, 25))
        pygame.draw.rect(window, GRAY, rect6, 2)
        pygame.draw.rect(window, GRAY, rect7, 2)

    pygame.display.flip()
