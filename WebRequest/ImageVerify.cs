using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;

namespace XXX
{
    class ImageVerify
    {
        private static void EraseNoiseDotAndGray(Bitmap bmp)
        {
            for (int w = 0; w < 50; w++)
            {
                for (int h = 0; h < 20; h++)
                {
                    if (bmp.GetPixel(w, h).R == 204) //为杂点，变为白色
                    {
                        bmp.SetPixel(w, h, Color.White);
                    }
                    else if (bmp.GetPixel(w, h).G != 255) //为数字所在点，变为黑色
                    {
                        bmp.SetPixel(w, h, Color.Black);
                    }
                }
            }
        }

        public static string GetStrFromBmp(string fileName)
        {

            Bitmap bmp = new Bitmap(Image.FromFile(fileName));

            string recordString=string.Empty;

            //EraseNoiseDotAndGray(bmp);

            //bmp.Save(@"now_no_noise_dot.bmp");

            //从左向右一列一列扫描，寻找四个数字的左右边界以切割  
            int verticalBlackContent = 0;

            int width = 0;

            int currentNumberIndex = 0;

            int[,] horizonAxisArray = new int[4, 2];

            //左右边界初始化  
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    horizonAxisArray[i, j] = 0;
                }
            }
            while (currentNumberIndex <= 3 && width < 50)
            {
                for (int h = 8; h < 18; h++)
                {
                    if (bmp.GetPixel(width, h).ToArgb() == Color.Black.ToArgb())
                    {
                        verticalBlackContent++;
                    }
                }

                if (horizonAxisArray[currentNumberIndex, 0] == 0 && verticalBlackContent > 0) //左边界  
                {
                    horizonAxisArray[currentNumberIndex, 0] = width;
                }
                else if (horizonAxisArray[currentNumberIndex, 0] != 0 && horizonAxisArray[currentNumberIndex, 1] == 0 && verticalBlackContent == 0) //右边界  
                {
                    horizonAxisArray[currentNumberIndex++, 1] = width - 1;
                }

                width = width + 1;

                verticalBlackContent = 0;
            }

            int[] tNumArr = new int[4]; //识别后的4个数字  

            //加载10个标准数字  
            Bitmap[] sNumArr = new Bitmap[10];

            for (int i = 0; i < 10; i++)
            {
                sNumArr[i] = new Bitmap(@"s" + i + ".bmp");
            }

            //分割出来的4个数字分别与等宽的标准数字相匹配  
            for (int i = 0; i < 4; i++)
            {
                int maxMatch = 0;

                Bitmap cutNum = bmp.Clone(new Rectangle(horizonAxisArray[i, 0], 8, horizonAxisArray[i, 1] - horizonAxisArray[i, 0] + 1, 10), PixelFormat.Format24bppRgb);

                for (int s = 0; s < 10; s++)
                {
                    if (cutNum.Width == sNumArr[s].Width) //等宽则匹配  
                    {
                        int curMatch = 0;

                        for (int j = 0; j < cutNum.Width; j++)
                        {
                            for (int k = 0; k < 10; k++)
                            {
                                if (cutNum.GetPixel(j, k).ToArgb() == sNumArr[s].GetPixel(j, k).ToArgb() &&

                                    cutNum.GetPixel(j, k).ToArgb() == Color.Black.ToArgb() &&

                                    sNumArr[s].GetPixel(j, k).ToArgb() == Color.Black.ToArgb())
                                {
                                    curMatch++;
                                }
                            }
                        }
                        if (curMatch > maxMatch)
                        {
                            maxMatch = curMatch;

                            tNumArr[i] = s;
                        }
                    }
                }
                recordString += tNumArr[i] + "";
            }

            return recordString;
        }

        public static string imageVerify(string filename)
        {
            UnCodebase un = new UnCodebase(new Bitmap(Image.FromFile(filename)));
            un.bmpobj = un.GetPicValidByValue(un.bmpobj, 128);

 

            un.GrayByPixels(); //灰度处理

            un.bmpobj.Save("image.jpg");
            un.GetPicValidByValue(128, 4); //得到有效空间
            Bitmap[] pics =un.GetSplitPics(4, 1);     //分割
            string[] arr = new string[4];
            for (int i = 0; i < 4; i++)
            {
                pics[i].Save("s" + i + ".jpg");
                arr[i]= un.GetSingleBmpCode(pics[i], 128);   //得到代码串
            }

            string picnum = getPicnums(arr);

            return picnum;
        }

       private static string[] ArrayList = new string[]{
              "00011100011111110110001111000001110000011100000111000001110000011100000111000001011000110111111100011100",  //0
              "00111000111110001111100000011000000110000001100000011000000110000001100000011000000110001111111111111111",  //1
              "01111100111111101000001100000011000000110000011000001100000110000011000001100000110000001111111111111111",  //2
              "01111100111111111000001100000011000001100111100001111110000001110000001100000011100001111111111001111100",  //3
              "00001100000111000001110000111100011011000110110010001100110011001111111110111111000011000000110000001100",  //4
              "11111111111111111100000011000000110000001111100011111110000001110000001100000011100001111111111001111100",  //5
              "00011110001111110110000101100000110000001101111011101111111000111100000101000001011000110110111100011110",  //6
              "01111010001111110000000100000000000000110000011000000100000011000000100000011000000110000011000000110000",  //7
              "00111110011111110110001101100011011100100011111000111110011001111100000111000001111000110111111100111110",  //8
              "00011100011110110110001111000001110000011010001101111111001111010000000100000001010000110101110000111100",  //9
              "00111000111111101000010010000011100000111000001110000011000000111000001110000011110001001111011000111000",  //10
              "00001100011111000111110000001100000011000000110000001100000011000000110000001100000011000111110101111111",  //11
              "11111000110111000000010000000110000001100000110000011000001100000110000011000000100000000101111011111110",  //12
              "10111000111111100000001000000110000011001111000011110100000011100000011000000110000011101111110011111000",  //13
              "00000110000011100000111000001110000101100011011000100110011001001111111111111111000001000000011000000110",  //14
              "11111110111111101000000010000000100000001111000011110100000011100000011000000110000011101111110011111000",  //15
              "00111100011111101100001011000000100000001011110011111110110001111000001110000011110001101111111000111100",  //16
              "11101111111111110000001100000010000001100000010000001000000110000001000000110000001100000110000001100000",  //17
              "01111100111111101100011011000110110001000111110001111100110011101000001110000011110001101101111001111100",  //18
              "01111000111111101100011010000011100000111100011111111111011110110000001100000100100001101111110001111000",  //19
              "00111100011111111100001110000001110000011110001101111111001101010000000000000011010000010111111000111100",  //20=9
              "00111000111111001100011010000011100000011000001100000011100000111000001010000011110001101111010000101000",  //21=0
              "00111000010111010110001111000001110000011110001101111111001111010000000100000011010000100111011000111100",  //22=9
              "00000110000010100000111000010110001101100011011001000110011001101011101111111111000001100000011000000110",  //23=4
              "00011110001011110110000101000000110000001101101011111101011000111100000110000001010000110101111000011110",  //24=6
              "00111100011111101110001101000001110000011110001101111111001111010000000100000011000000110101101000111100",  //25=9
              "11111000111100000000011000000100000000100000110000011000001100000110000001000000100000001111110011111110"   //26=2
          };

        public static string getPicnums(string[] arr)
        {
            string Code = "";
            for (int i = 0; i < 4; i++)
            {
                string code = arr[i];   //得到代码串

                for (int arrayIndex = 0; arrayIndex < ArrayList.Length; arrayIndex++)
                {
                    //逐点判断特征码是否相同，允许误差!
                    char temp1, temp2;
                    int point = 0;
                    if (ArrayList[arrayIndex].Equals(code))
                    {
                        point = 0;
                        if (arrayIndex > 9)
                        {
                            if (arrayIndex == 20 || arrayIndex == 22 || arrayIndex == 25)
                            {
                                Code = Code + "9";
                            }
                            else if (arrayIndex == 21)
                            {
                                Code = Code + "0";
                            }
                            else if (arrayIndex == 23)
                            {
                                Code = Code + "4";
                            }
                            else if (arrayIndex == 24)
                            {
                                Code = Code + "6";
                            }
                            else if (arrayIndex == 26)
                            {
                                Code = Code + "2";
                            }
                            else
                            {
                                Code = Code + (arrayIndex - 10).ToString();
                            }
                        }
                        else
                        {
                            Code = Code + arrayIndex.ToString();
                        }
                        break;
                    }
                    else
                    {
                        //将字符串数组，直接转为单个字符进行对比，并记录不相同的点
                        for (int Comparison = 0; Comparison < code.Length; Comparison++)
                        {
                            temp1 = arr[i][Comparison];
                            temp2 = ArrayList[arrayIndex][Comparison];
                            if (temp1 != temp2)
                            {
                                point = point + 1;
                            }
                        }
                    }

                    //当不相同点的值小于10的时候，也就是说误差点小于10的时候则直接等于此数字，否则将跳出循环继续对下一个特征码进行判断
                    if (point < 10)
                    {
                        if (arrayIndex > 9)
                        {
                            if (arrayIndex == 20 || arrayIndex == 22 || arrayIndex == 25)
                            {
                                Code = Code + "9";
                            }
                            else if (arrayIndex == 21)
                            {
                                Code = Code + "0";
                            }
                            else if (arrayIndex == 23)
                            {
                                Code = Code + "4";
                            }
                            else if (arrayIndex == 24)
                            {
                                Code = Code + "6";
                            }
                            else if (arrayIndex == 26)
                            {
                                Code = Code + "2";
                            }
                            else
                            {
                                Code = Code + (arrayIndex - 10).ToString();
                            }
                        }
                        else
                        {
                            Code = Code + arrayIndex.ToString();
                        }
                        break;
                    }
                }
            }
            return Code;
        }
    }
}
