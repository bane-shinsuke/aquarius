package net.bane.aquarius.rest;

import java.io.File;
import java.io.IOException;
import java.util.Calendar;

import javax.ws.rs.FormParam;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

//The Java class will be hosted at the URI path "/helloworld"
@Path("/power")
public class TurnPower {
    @POST
    @Produces(MediaType.TEXT_PLAIN)
    public String turnPower( @FormParam("value") String value) {
    	
    	
    	Calendar now = Calendar.getInstance(); //インスタンス化

    	System.out.println(value);
    	
    	if(value != null) {
    		File file = new File("c:/temp/aquarius-work/" + value);
    		try {
				file.createNewFile();
			} catch (IOException e) {
				e.printStackTrace();
			}
    		
    	}
        int h = now.get(now.HOUR_OF_DAY);//時を取得
        int m = now.get(now.MINUTE);     //分を取得
        int s = now.get(now.SECOND);      //秒を取得
        System.out.println(h+"時"+m+"分"+s+"秒");
        return "Success!! " + h+":"+m+":"+s;
    }
}