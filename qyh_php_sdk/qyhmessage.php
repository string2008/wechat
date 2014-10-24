<?php
/**
 *	微信公众平台企业号验证SDK
 *  @QQ  94560716
 *  @link https://github.com/string2008/wechat
 *  @version 1.0
 *  
 *       
 *
 */
	 
	//记录log
	 function logTrace($text)
	{
		$str= date("Y-m-d H:i:s")." $text\r\n";
		file_put_contents('log.txt',$str,FILE_APPEND);
	}
	
	//回复文本消息
	function makeText($fromUsername,$toUsername,$content)
	{
		//logTrace("qyemessage::makeText----toUsername:$toUsername");		
		$createTime = time();
		$textTpl = "<xml>
			<ToUserName><![CDATA[%s]]></ToUserName>
			<FromUserName><![CDATA[%s]]></FromUserName>
			<CreateTime>%s</CreateTime>
			<MsgType><![CDATA[text]]></MsgType>
			<Content><![CDATA[%s]]></Content>
			</xml>";
		$resultStr = sprintf($textTpl,$fromUsername,$toUsername,$createTime,$content);	
		//logTrace("qyemessage::makeText----resultStr:$resultStr");		
		return $resultStr;
	}
	
	
	//根据数组参数回复图文消息 
	function makeNews($fromUsername,$toUsername,$newsData=array())
	{
		$createTime = time();
		$newTplHeader = '<xml>
			<ToUserName><![CDATA[%s]]></ToUserName>
			<FromUserName><![CDATA[%s]]></FromUserName>
			<CreateTime>%s</CreateTime>
			<MsgType><![CDATA[news]]></MsgType>
			<ArticleCount>%s</ArticleCount>
			<Articles>';
		$newTplItem = '<item>
			<Title><![CDATA[%s]]></Title>
			<Description><![CDATA[%s]]></Description>
			<PicUrl><![CDATA[%s]]></PicUrl>
			<Url><![CDATA[%s]]></Url>
			</item>';
		$newTplFoot = '</Articles>
			</xml>';
			$Content="";			    
		$itemsCount = count($newsData);
		$itemsCount = $itemsCount < 10 ? $itemsCount : 10;//微信公众平台图文回复的消息一次最多10条
		if ($itemsCount) 
		{
			foreach ($newsData as $key => $item) 
			{
				if ($key<=9) 
			{
					$Content .= sprintf($newTplItem,$item['Title'],$item['Description'],$item['PicUrl'],$item['Url']);
				}
			}
		}
		$header = sprintf($newTplHeader,$fromUsername,$toUsername,$createTime,$itemsCount);
		return $header . $Content . $newTplFoot;
	}
			
	//回复图片消息
	function makeImage($fromUsername,$toUsername,$mediaID)
	{
		$createTime = time();
		$textTpl = "<xml>
			<ToUserName><![CDATA[%s]]></ToUserName>
			<FromUserName><![CDATA[%s]]></FromUserName>
			<CreateTime>%s</CreateTime>
			<MsgType><![CDATA[image]]></MsgType>
			<Image>
				<MediaId><![CDATA[%s]]></MediaId>
			</Image>
			</xml>";
		$resultStr= sprintf($textTpl,$fromUsername,$toUsername,$createTime,$mediaID);		
		return $resultStr;
	}
	
	//回复语音消息
	function makeVoice($fromUsername,$toUsername,$mediaID)
	{
		$createTime = time();
		$textTpl = "<xml>
			<ToUserName><![CDATA[%s]]></ToUserName>
			<FromUserName><![CDATA[%s]]></FromUserName>
			<CreateTime>%s</CreateTime>
			<MsgType><![CDATA[voice]]></MsgType>
			<Voice>
			 <MediaId><![CDATA[%s]]></MediaId>
			</Voice>
			</xml>";
		$resultStr= sprintf($textTpl,$fromUsername,$toUsername,$createTime,$mediaID);		
		return $resultStr;
	}
	
	//回复视频消息
	function makeVideo($fromUsername,$toUsername,$mediaID,$title,$description)
	{
		$createTime = time();
		$textTpl = "<xml>
			<ToUserName><![CDATA[%s]]></ToUserName>
			<FromUserName><![CDATA[%s]]></FromUserName>
			<CreateTime>%s</CreateTime>
			<MsgType><![CDATA[video]]></MsgType>
			<Video>
				<MediaId><![CDATA[%s]]></MediaId>
				<Title><![CDATA[%s]]></Title>
				<Description><![CDATA[%s]]></Description>
			</Video>
			</xml>";
		$resultStr= sprintf($textTpl,$fromUsername,$toUsername,$createTime,$mediaID,$title,$description);		
		return $resultStr;
	}
	
	//回复音乐消息
	function makeMusic($fromUsername,$toUsername,$mediaID
						,$title,$description,$musicURL,$hqMusicURL,$thumbMediaId)
	{
		$createTime = time();
		$textTpl = "<xml>
			<ToUserName><![CDATA[%s]]></ToUserName>
			<FromUserName><![CDATA[%s]]></FromUserName>
			<CreateTime>%s</CreateTime>
			<MsgType><![CDATA[music]]></MsgType>
			<Music>
				<Title><![CDATA[%s]]></Title>
				<Description><![CDATA[%s]]></Description>
				<MusicUrl><![CDATA[MUSIC_Url]]></MusicUrl>
				<HQMusicUrl><![CDATA[HQ_MUSIC_Url]]></HQMusicUrl>
				<ThumbMediaId><![CDATA[media_id]]></ThumbMediaId>
			</Music>
			</xml>";
		$resultStr= sprintf($textTpl,$fromUsername,$toUsername,$createTime,$mediaID
							,$title,$description,$musicURL,$hqMusicURL,$thumbMediaId);		
		return $resultStr;
	}
	
	//CURL POST
	function curlPostData($url, $header, $data)
	{
		$curl = curl_init($url) ;
		curl_setopt($curl, CURLOPT_URL, $url); // 要访问的地址
		curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, FALSE); // 对认证证书来源的检查
		curl_setopt($curl, CURLOPT_SSL_VERIFYHOST, FALSE); // 从证书中检查SSL加密算法是否存在
		curl_setopt($curl, CURLOPT_USERAGENT, 'Mozilla/5.0 (compatible; MSIE 5.01; Windows NT 5.0)'); // 模拟用户使用的浏览器
		curl_setopt($curl, CURLOPT_POST, 1); // 发送一个常规的Post请求
		curl_setopt($curl, CURLOPT_POSTFIELDS, $data); // Post提交的数据包x
		curl_setopt($curl, CURLOPT_TIMEOUT, 30); // 设置超时限制防止死循环
		curl_setopt($curl, CURLOPT_HEADER, $header); // 显示返回的Header区域内容
		curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1); // 获取的信息以文件流的形式返回
		$result = curl_exec($curl) ;
		if (curl_errno($curl)) 
		{
			return 'Errno'.curl_error($curl);
		}

		curl_close($curl) ;
		return $result;
	}	
	
	
	

?>