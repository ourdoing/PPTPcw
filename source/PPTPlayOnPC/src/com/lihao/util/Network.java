/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.lihao.util;

import com.becivells.ui.Main;
import java.lang.reflect.Array;
import java.net.Inet4Address;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 *
 * @author beciv
 */
public  class Network {

    /**
     *取得主机的ip地址
     */
    public static ArrayList<String> getIpaddrs() throws UnknownHostException, SocketException {
        ArrayList<String> ipstr = new ArrayList<String>();
        Enumeration allNetInterfaces = NetworkInterface.getNetworkInterfaces();
        InetAddress ip = null;
        while (allNetInterfaces.hasMoreElements())
        {
            NetworkInterface netInterface = (NetworkInterface) allNetInterfaces.nextElement();
            Enumeration addresses = netInterface.getInetAddresses();
            while (addresses.hasMoreElements())
            {
                ip = (InetAddress) addresses.nextElement();
                if (ip != null && ip instanceof Inet4Address)
                {
                     Pattern p = Pattern.compile("([0-9.]{7,})$");
                    Matcher m = p.matcher(ip.getHostAddress());
                     if(m.matches()){
                     ipstr.add(m.group(1));
    
                     }
                }
            }
        }
        return ipstr;
    }
}
