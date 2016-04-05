/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.becivells.qrcode;

import com.google.zxing.BarcodeFormat;
import com.google.zxing.MultiFormatWriter;
import com.google.zxing.client.j2se.MatrixToImageWriter;
import com.google.zxing.common.BitMatrix;
import com.lihao.util.Network;
import java.awt.image.BufferedImage;
import java.io.File;
import java.util.Hashtable;

/**
 *
 * @author beciv
 */
public class EnQcode {
    public  static void encode(String s){
    try {  
        
            String str =s;// 二维码内容  
            String path = "qcode.png";  
            BitMatrix byteMatrix;  
            byteMatrix = new MultiFormatWriter().encode(new String(str.getBytes("GBK"),"iso-8859-1"),  
                    BarcodeFormat.QR_CODE, 130, 130);  
            File file = new File(path);  
              
            MatrixToImageWriter.writeToFile(byteMatrix, "png", file);  
        } catch (Exception e) {  
            e.printStackTrace();  
        }  
    } 
  //  public static  encodeimg()
    public static void main(String[] args) {
        encode("dfsdfsd");
    }
  
    }
