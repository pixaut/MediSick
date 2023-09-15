import pygame
import math
import pathlib
from pathlib import Path
import cv2
pygame.init()
window = pygame.display.set_mode((900, 750))
pygame.display.set_caption("Paint/ai v. 1.5")
clock = pygame.time.Clock()

WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
GRAY = (105, 105, 105)
GRAY2 = (62, 68, 82)
GRAYD = (65, 65, 65)
RED = (210, 30, 30)
GREEN = (77, 209, 29)
BLUE = (20, 179, 228)
PURPLE = (204, 204, 255)
PURPLE2 = (204, 204, 255)
PURPLE3 = (204, 204, 255)
PURPLE4 = (204, 204, 255)
DARKBLUE = (35, 39, 46)
DARKBLUE2 = (41, 46, 58)
GRAYBLUE = (105, 110, 118)
GRAYBLUE2 = (156, 166, 181)

rect1 = pygame.Rect((80, 780, 300, 100))
rect2 = pygame.Rect((500, 780, 300, 100))
rect3 = pygame.Rect((120, 50, 640, 640))
rect4 = pygame.Rect((800, 50, 50, 50))
rect5 = pygame.Rect((800, 100, 50, 650))
rect7 = pygame.Rect((800, 50, 50, 641))
rect8 = pygame.Rect((120, 50, 640, 640))
rect9 = pygame.Rect((85, 785, 290, 90))
rect10 = pygame.Rect((505, 785, 290, 90))
rect11 = pygame.Rect((820, 0, 80, 25))


pygame.font.init()
font1 = pygame.font.SysFont("Bahnschrift", 65)
font4 = pygame.font.SysFont("Bahnschrift", 60)
font2 = pygame.font.SysFont("Arial", 40)
font3 = pygame.font.SysFont("Arial", 25)
font5 = pygame.font.SysFont("Verdana", 10)

savefilepic = font1.render("SAVE", False, (GRAYBLUE))
clearpic = font1.render("CLEAR", False, (GRAYBLUE))

savefilepic2 = font1.render("SAVE", False, (GRAYBLUE2))
clearpic2 = font1.render("CLEAR", False, (GRAYBLUE2))

savepic1 = pygame.image.load(r"save1.png")
savepic2 = pygame.image.load(r"save2.png")

clearpic1 = pygame.image.load(r"clear1.png")
clearpic2 = pygame.image.load(r"clear2.png")

penpic1 = pygame.image.load(r"pen1.png")
penpic2 = pygame.image.load(r"pen2.png")

erasepic1 = pygame.image.load(r"erase1.png")
erasepic2 = pygame.image.load(r"erase2.png")

thicknesspic1l = pygame.image.load(r"thickness1l.png")
thicknesspic2l = pygame.image.load(r"thickness2l.png")
thicknesspic3l = pygame.image.load(r"thickness3l.png")
thicknesspic1d = pygame.image.load(r"thickness1d.png")
thicknesspic2d = pygame.image.load(r"thickness2d.png")
thicknesspic3d = pygame.image.load(r"thickness3d.png")


letterspic = pygame.image.load(r"ls.png")


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
penset = True
thikpos = 1
letter = False
flag = False
flag2 = False
flag3 = False
flag4 = False
screensaver = False
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
            pygame.draw.rect(window, GRAY2, rect, 1)


def drawgui():
    window.fill(DARKBLUE)
    pygame.draw.rect(window, WHITE, rect8, 2000)
    pygame.draw.rect(window, GRAY, rect3, 1)
    window.blit(savepic1, (30, 50))
    window.blit(clearpic1, (30, 110))
    window.blit(penpic1, (30, 190))
    window.blit(erasepic1, (30, 250))
    window.blit(thicknesspic1d, (30, 310))


def drawletgui():
    window.blit(letterspic, (800, 50))
    # pygame.draw.rect(window, GRAY2, rect7, 1)
    rect6 = pygame.Rect((800, (letternum * 24.615)+25.5, 50, 25))
    pygame.draw.rect(window, GRAYBLUE, rect6, 2)


def clear():
    pygame.draw.rect(window, WHITE, rect8, 2000)
    # drawGrid()
    pygame.draw.rect(window, GRAY, rect3, 1)

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


def cleansquare(x, y, c, t):
    if (t == 1):
        rect = pygame.Rect((x * 10), (y * 10), 10, 10)
        pygame.draw.rect(window, c, rect, 5)

    if (t == 2):
        rect = pygame.Rect((x * 10), (y * 10), 20, 20)
        pygame.draw.rect(window, c, rect, 10)

    if (t == 3):
        rect = pygame.Rect((x * 10), (y * 10), 30, 30)
        pygame.draw.rect(window, c, rect, 15)


def drawsquare(x, y, t):
    if (t == 1):
        matrix[round((pos[1] - 50) / 10)][round((pos[0] - 120) / 10)] = 1
        rect = pygame.Rect(
            (round((pos[0]) / 10) * 10), (round((pos[1]) / 10) * 10), 10, 10)
        pygame.draw.rect(window, BLACK, rect, 5)

    if (t == 2):
        rect = pygame.Rect((x * 10), (y * 10), 20, 20)
        pygame.draw.rect(window, BLACK, rect, 10)

    if (t == 3):
        rect = pygame.Rect((x * 10), (y * 10), 30, 30)
        pygame.draw.rect(window, BLACK, rect, 15)


drawgui()
drawletgui()
while app:
    print(thikpos)
    pygame.draw.rect(window, DARKBLUE, rect11, 50)
    fps_c = font5.render(
        "FPS: " + str(round(clock.get_fps())), False, (RED))
    window.blit(fps_c, (840, 3))

    clock.tick(0)
    pressed = pygame.mouse.get_pressed()
    pos = pygame.mouse.get_pos()
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            app = False
    # print(pos)
    if (pos[0] > 120 and pos[0] < 755) and (pos[1] > 50 and pos[1] < 685):

        if pressed[0] and penset:
            drawsquare(pos[0], pos[1], thikpos)

        if pressed[0] and not penset:
            matrix[round((pos[1] - 50) / 10)][round((pos[0] - 120) / 10)] = 0
            cleansquare(round((pos[0]) / 10), round((pos[1]) / 10), WHITE, 3)

    if (
        pressed[0]
        and flag2 == False
        and (pos[0] > 30 and pos[0] < 80)
        and (pos[1] > 50 and pos[1] < 100)
    ):
        save()
        flag2 = True
    if pressed[0] == False:
        flag2 = False

    if (
        (pressed[0])
        and (pos[0] > 30 and pos[0] < 80)
        and (pos[1] > 110 and pos[1] < 160)
    ):
        clear()

    if (
        (pressed[0])
        and (pos[0] > 30 and pos[0] < 80)
        and (pos[1] > 190 and pos[1] < 240)
    ):
        penset = True

    if (
        (pressed[0])
        and (pos[0] > 30 and pos[0] < 80)
        and (pos[1] > 250 and pos[1] < 300)
    ):
        penset = False

    if (pos[0] > 30 and pos[0] < 80) and (pos[1] > 50 and pos[1] < 100):
        window.blit(savepic2, (30, 50))
    else:
        window.blit(savepic1, (30, 50))

    if (pos[0] > 30 and pos[0] < 80) and (pos[1] > 110 and pos[1] < 160):
        window.blit(clearpic2, (30, 110))
    else:
        window.blit(clearpic1, (30, 110))

    if (pos[0] > 30 and pos[0] < 80) and (pos[1] > 190 and pos[1] < 240) or (penset):
        window.blit(penpic2, (30, 190))
    else:
        window.blit(penpic1, (30, 190))

    if (pos[0] > 30 and pos[0] < 80) and (pos[1] > 250 and pos[1] < 300) or (not penset):
        window.blit(erasepic2, (30, 250))
    else:
        window.blit(erasepic1, (30, 250))

    if (pos[0] > 40 and pos[0] < 58) and (pos[1] > 327 and pos[1] < 442) and pressed[0]:
        thikpos = round((pos[1]-308)/38.333)

    if (pos[0] > 40 and pos[0] < 58) and (pos[1] > 327 and pos[1] < 442):

        if (thikpos == 1):
            window.blit(thicknesspic1l, (30, 310))
        if (thikpos == 2):
            window.blit(thicknesspic2l, (30, 310))
        if (thikpos == 3):
            window.blit(thicknesspic3l, (30, 310))
    else:
        if (thikpos == 1):
            window.blit(thicknesspic1d, (30, 310))
        if (thikpos == 2):
            window.blit(thicknesspic2d, (30, 310))
        if (thikpos == 3):
            window.blit(thicknesspic3d, (30, 310))

    if (
        (pressed[0])
        and (pos[0] > 800 and pos[0] < 850)
        and (pos[1] > 50 and pos[1] < 690)
    ):
        letternum = math.floor(((pos[1] - 50) / 24.615) + 1)
        print(letternum)
        # print(letterset[letternum - 1], "    ", lett ernum - 1)
        drawletgui()
        rect6 = pygame.Rect((800, (letternum * 24.615)+25.5, 50, 25))
        pygame.draw.rect(window, GRAY2, rect7, 1)
        pygame.draw.rect(window, GRAYBLUE, rect6, 2)

    pygame.display.flip()
