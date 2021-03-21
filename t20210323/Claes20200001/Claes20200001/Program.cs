using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Charlotte.Commons;
using Charlotte.Tests;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2);
		}

		private void Main2(ArgsReader ar)
		{
			// -- choose one --

			Main3();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();

			Common.OpenOutputDirIfCreated();
		}

		#region TREE

		private static string TREE = @"

C:\BlueFish\BlueFish\HTT\stackprobe\home\_kit
C:\BlueFish\BlueFish\HTT\stackprobe\home\_kit\Extra
C:\BlueFish\BlueFish\HTT\stackprobe\home\_kit\Extra\_hidden
C:\BlueFish\BlueFish\HTT\stackprobe\home\_kit\Extra\_hidden\_diamond
C:\BlueFish\BlueFish\HTT\stackprobe\home\_kit\Factory
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_20180720_205718215486452328372537439199015667841
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_20181013_311549942628164494905645464845172719885
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_20181106_8493465661737846
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_20181130_8493465661737846
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_20200702_672916781210407160256752509023899888492
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_20200712_258763925281503227501733467067783361102
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_alice
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_alice\application
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_alice\game
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_reflection
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\_test
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191020
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191021
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191021\picked-out
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\020x020_0001
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\020x020_0002
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\020x020_0003
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\020x020_0004
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\020x020_0005
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\030x030_0001
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\030x030_0002
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\030x030_0003
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\030x030_0004
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\030x030_0005
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\050x050_0001
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\050x050_0002
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\050x050_0003
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\050x050_0004
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\050x050_0005
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\100x100_0001
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\100x100_0002
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\100x100_0003
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\100x100_0004
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\100x100_0005
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\150x150_0001
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\150x150_0002
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\150x150_0003
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\150x150_0004
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\150x150_0005
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\200x200_0001
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\200x200_0002
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\200x200_0003
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\200x200_0004
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191029\200x200_0005
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191031
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20191107
C:\BlueFish\BlueFish\HTT\stackprobe\home\_rosetta\Hatena\20200902
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp\2020.107.89442
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp\2020.129.82891
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp\2020.231.15257
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp\2020.231.68047
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp\2020.239.27350
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp\2020.263.80767
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp\2020.264.26677
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp\2020.269.10169
C:\BlueFish\BlueFish\HTT\stackprobe\home\AccessLamp\2020.271.24055
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.107.89482
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.123.96003
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.129.83067
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.207.94060
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.213.19188
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.213.20575
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.215.90657
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.216.14946
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.216.46076
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.216.94946
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.220.60362
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.231.68745
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.239.27215
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.245.72727
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.247.56353
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.263.80628
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.264.26542
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.268.96394
C:\BlueFish\BlueFish\HTT\stackprobe\home\audiofile2mp4\2020.271.23893
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.107.89516
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.129.81433
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.231.13556
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.231.61273
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.239.23283
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.239.25583
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.263.78743
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.264.24761
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.268.94282
C:\BlueFish\BlueFish\HTT\stackprobe\home\Chroco\2020.271.22001
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.107.89624
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.124.41776
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.129.82434
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.231.14714
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.231.66095
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.239.26680
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.263.80098
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.264.26035
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.268.95767
C:\BlueFish\BlueFish\HTT\stackprobe\home\CPortFwd\2020.271.23335
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient\2020.107.89657
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient\2020.129.82926
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient\2020.231.15296
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient\2020.231.68186
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient\2020.239.27383
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient\2020.263.80801
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient\2020.264.26711
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient\2020.269.10213
C:\BlueFish\BlueFish\HTT\stackprobe\home\DDnsClient\2020.271.24095
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.107.89695
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.124.41370
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.129.81690
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.151.69792
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.231.14327
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.231.65143
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.239.26249
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.263.79645
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.264.25611
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.268.95254
C:\BlueFish\BlueFish\HTT\stackprobe\home\Dungeon\2020.271.22871
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc\2020.107.89730
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc\2020.129.82799
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc\2020.231.15182
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc\2020.231.67767
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc\2020.239.26571
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc\2020.263.79982
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc\2020.264.25927
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc\2020.268.95630
C:\BlueFish\BlueFish\HTT\stackprobe\home\FatCalc\2020.271.23211
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher\2020.107.89764
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher\2020.129.82396
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher\2020.231.14677
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher\2020.231.65957
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher\2020.239.26645
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher\2020.263.80060
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher\2020.264.25999
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher\2020.268.95722
C:\BlueFish\BlueFish\HTT\stackprobe\home\fCipher\2020.271.23295
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.107.89797
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.129.81535
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.231.13747
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.231.61971
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.239.23487
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.239.25743
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.263.78948
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.264.24986
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.268.94500
C:\BlueFish\BlueFish\HTT\stackprobe\home\HechimaClient\2020.271.22194
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.108.24202
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.116.65327
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.117.40300
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.124.61996
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.125.85047
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.129.81251
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.130.74817
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.151.70513
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.192.42569
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.231.18210
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.231.77387
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.239.30536
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.264.30752
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.269.10880
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT_RPC\2020.271.27278
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.107.89839
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.124.41596
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.124.61581
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.127.73591
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.129.80730
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.130.36853
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.130.53946
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.130.74515
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.130.78590
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.131.72348
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.136.61935
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.192.42199
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.231.14755
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.231.66238
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.232.25885
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.239.26720
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.263.80141
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.264.26077
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.268.95822
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTT\2020.271.23382
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.107.89883
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.124.61711
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.127.73697
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.129.80856
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.130.36960
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.130.54051
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.130.74607
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.130.78688
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.131.72448
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.136.62092
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.151.70209
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.192.42323
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.231.66379
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.232.25972
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.239.26765
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.263.80189
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.264.26123
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.268.95886
C:\BlueFish\BlueFish\HTT\stackprobe\home\HTTDir\2020.271.23433
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.107.89926
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.124.41250
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.129.81466
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.143.62518
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.231.13594
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.231.61414
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.239.23326
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.239.25615
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.263.78787
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.264.24800
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.268.94328
C:\BlueFish\BlueFish\HTT\stackprobe\home\KCamera\2020.271.22042
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.107.89968
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.129.82483
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.231.14811
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.231.66539
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.239.26814
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.245.72643
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.247.56267
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.263.80237
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.264.26170
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.268.95945
C:\BlueFish\BlueFish\HTT\stackprobe\home\Kirara\2020.271.23485
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch\2020.107.90013
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch\2020.129.82530
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch\2020.231.14873
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch\2020.231.66719
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch\2020.239.26862
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch\2020.263.80290
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch\2020.264.26219
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch\2020.268.96003
C:\BlueFish\BlueFish\HTT\stackprobe\home\MusBatch\2020.271.23539
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.107.90048
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.129.81575
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.231.13782
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.231.62112
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.239.23531
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.239.25778
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.263.78987
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.264.25022
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.268.94548
C:\BlueFish\BlueFish\HTT\stackprobe\home\MutectorDemo\2020.271.22236
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.107.90083
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.123.96044
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.129.83104
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.210.87481
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.220.60400
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.231.68886
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.239.27249
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.245.72774
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.247.56400
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.263.80663
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.264.26576
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.269.10037
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuconv\2020.271.23932
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.107.90118
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.123.96096
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.129.83141
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.231.15419
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.231.69026
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.239.27283
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.263.80698
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.264.26610
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.269.10081
C:\BlueFish\BlueFish\HTT\stackprobe\home\natsuedit\2020.271.23975
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.107.90150
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.129.81617
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.231.13815
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.231.62250
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.239.23571
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.239.25811
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.263.79022
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.264.25055
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.268.94594
C:\BlueFish\BlueFish\HTT\stackprobe\home\NectarDemo\2020.271.22273
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.107.90182
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.129.81500
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.231.13707
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.231.61691
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.239.23366
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.239.25646
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.263.78827
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.264.24875
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.268.94370
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaver\2020.271.22080
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd\2020.230.93160
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd\2020.231.61828
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd\2020.234.85097
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd\2020.239.23406
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd\2020.239.25678
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd\2020.263.78865
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd\2020.264.24918
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd\2020.268.94412
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverCmd\2020.271.22117
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.155.29657
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.155.80770
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.231.13673
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.231.61553
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.239.23446
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.239.25710
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.263.78904
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.264.24951
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.268.94455
C:\BlueFish\BlueFish\HTT\stackprobe\home\NoScreenSaverMusMv\2020.271.22155
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen\2020.107.90223
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen\2020.129.82678
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen\2020.231.15097
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen\2020.231.67325
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen\2020.239.27092
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen\2020.263.80498
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen\2020.264.26416
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen\2020.268.96238
C:\BlueFish\BlueFish\HTT\stackprobe\home\NumpleGen\2020.271.23753
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.107.90265
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.120.39410
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.124.41463
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.129.82963
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.158.65221
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.158.74687
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.159.48723
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.159.52434
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.159.88317
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.171.67079
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.192.40140
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.231.68330
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.239.27471
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.263.80838
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.264.26747
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.269.10261
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime4096\2020.271.24136
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64\2020.107.90298
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64\2020.129.82997
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64\2020.231.15343
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64\2020.231.68467
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64\2020.239.27512
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64\2020.263.80873
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64\2020.264.26782
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64\2020.269.10305
C:\BlueFish\BlueFish\HTT\stackprobe\home\Prime64\2020.271.24174
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.107.90329
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.124.41306
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.129.81652
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.231.13846
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.231.62387
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.239.23611
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.239.25842
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.263.79056
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.264.25087
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.268.94639
C:\BlueFish\BlueFish\HTT\stackprobe\home\SimplePaint\2020.271.22311
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku\2020.107.90364
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku\2020.129.82626
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku\2020.231.15040
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku\2020.231.67172
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku\2020.239.27042
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku\2020.263.80443
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku\2020.264.26365
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku\2020.268.96177
C:\BlueFish\BlueFish\HTT\stackprobe\home\Sudoku\2020.271.23698
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.107.90404
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.124.41517
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.129.82721
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.231.15147
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.231.67475
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.234.89491
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.239.27137
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.263.80545
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.264.26462
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.268.96294
C:\BlueFish\BlueFish\HTT\stackprobe\home\TCalc\2020.271.23804
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools\2020.107.90450
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools\2020.129.83033
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools\2020.231.15382
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools\2020.231.68606
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools\2020.239.27546
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools\2020.263.80907
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools\2020.264.26816
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools\2020.269.10348
C:\BlueFish\BlueFish\HTT\stackprobe\home\TunnelTools\2020.271.24213
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.107.90487
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.129.82761
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.226.84250
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.226.94131
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.226.95682
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.227.16729
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.227.45634
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.228.54493
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.229.73430
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.229.85050
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.229.89091
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.230.10619
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.231.67623
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.235.88295
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.235.94146
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.236.11881
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.239.14963
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.239.27176
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.263.80586
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.264.26502
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.268.96345
C:\BlueFish\BlueFish\HTT\stackprobe\home\UnrealRemoco\2020.271.23849
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.107.90531
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.124.41680
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.124.61642
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.127.73645
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.129.80796
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.130.36911
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.130.54002
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.130.74564
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.130.78640
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.131.72403
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.132.78605
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.132.79588
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.132.81760
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.136.62039
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.151.70248
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.192.42279
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.231.14930
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.231.66860
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.232.26027
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.239.26964
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.263.80399
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.264.26323
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.268.96127
C:\BlueFish\BlueFish\HTT\stackprobe\home\Uploader\2020.271.23651
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat\2020.107.90565
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat\2020.129.82856
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat\2020.231.15217
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat\2020.231.67907
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat\2020.239.26606
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat\2020.263.80020
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat\2020.264.25962
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat\2020.268.95675
C:\BlueFish\BlueFish\HTT\stackprobe\home\WChat\2020.271.23253
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.107.90598
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.123.95749
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.129.83176
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.231.15459
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.231.69166
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.239.27317
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.263.80733
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.264.26644
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.269.10125
C:\BlueFish\BlueFish\HTT\stackprobe\home\WindRect\2020.271.24016

";

		#endregion

		private void Main3()
		{
			string[] dirs = SCommon.TextToLines(TREE)
				.Where(line => line != "")
				.Select(line => line.Substring(@"C:\BlueFish\BlueFish\HTT\stackprobe\home\".Length))
				.ToArray();

			string destRootDir = Path.Combine(Common.GetOutputDir(), "sp_home");

			SCommon.CreateDir(destRootDir);
			MakeIndex(destRootDir, "/");

			foreach (string dir in dirs)
			{
				string destDir = Path.Combine(destRootDir, dir);

				SCommon.CreateDir(destDir);
				MakeIndex(destDir, ":58946/" + dir.Replace('\\', '/'));
			}
		}

		private void MakeIndex(string destDir, string dirPart)
		{
			string indexFile = Path.Combine(destDir, "index.html");
			string url = "http://stackprobe.ccsp.mydns.jp" + dirPart;

			File.WriteAllText(
				indexFile,
				@"<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
<!--
<meta http-equiv=""refresh"" content=""0;url=" + url + @"""/>
-->
</head>
<body>
<h1>移転しました。</h1>
移転先はこちら ⇒ <a href=""" + url + @""">" + url + @"</a>
</body>
</html>
",
				Encoding.UTF8
				);
		}
	}
}
