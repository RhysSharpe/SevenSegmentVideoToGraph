﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SevenSegmentVideoToGraph.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SevenSegmentVideoToGraph.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Extracting finished in {0} seconds..
        /// </summary>
        public static string AppInfoExtractingComplete {
            get {
                return ResourceManager.GetString("AppInfoExtractingComplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Extracting Frames {0}%….
        /// </summary>
        public static string AppInfoExtractingFrames {
            get {
                return ResourceManager.GetString("AppInfoExtractingFrames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Extracting was stopped early at {0}% within {1} seconds….
        /// </summary>
        public static string AppInfoExtractingStopped {
            get {
                return ResourceManager.GetString("AppInfoExtractingStopped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Critical error!
        ///
        ///The application failed to find the images at the selected location. The extraction files were tampered with or there might be an issue with the drive.
        ///
        ///The application will now close..
        /// </summary>
        public static string AppInfoMissingFiles {
            get {
                return ResourceManager.GetString("AppInfoMissingFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OCR process complete..
        /// </summary>
        public static string AppInfoOcrComplete {
            get {
                return ResourceManager.GetString("AppInfoOcrComplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Images have now been processed..
        /// </summary>
        public static string AppInfoProcessingCompleted {
            get {
                return ResourceManager.GetString("AppInfoProcessingCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a cropped video (MP4) to translate into a CSV file or graph. You may otherwise select a folder containing previously extracted frames and press the load button. The frames must be named &quot;frame-X&quot; where &apos;X&apos; is the millisecond timestamp.
        ///
        ///The video or frames should only contain the 7 segment numbers as anything else will likely reduce the OCR accuracy. This includes any non-numerical shapes and objects with similar brightness as the digits.
        ///
        ///Adjust the frame interval slider to increase or decrease  [rest of string was truncated]&quot;;.
        /// </summary>
        public static string AppInstructions {
            get {
                return ResourceManager.GetString("AppInstructions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Seven Segment Video to Graph - Error.
        /// </summary>
        public static string AppTitleError {
            get {
                return ResourceManager.GetString("AppTitleError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Seven Segment Video to Graph - Finished.
        /// </summary>
        public static string AppTitleFinished {
            get {
                return ResourceManager.GetString("AppTitleFinished", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Seven Segment Video to Graph - Help.
        /// </summary>
        public static string AppTitleHelp {
            get {
                return ResourceManager.GetString("AppTitleHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Seven Segment Video to Graph.
        /// </summary>
        public static string AppTitleMain {
            get {
                return ResourceManager.GetString("AppTitleMain", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Seven Segment Video to Graph - Question.
        /// </summary>
        public static string AppTitleQuestion {
            get {
                return ResourceManager.GetString("AppTitleQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Seven Segment Video to Graph - Results.
        /// </summary>
        public static string AppTitleResults {
            get {
                return ResourceManager.GetString("AppTitleResults", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Seven Segment Video to Graph - Manul Assisted Frames.
        /// </summary>
        public static string AppTitleUnprocessedFrames {
            get {
                return ResourceManager.GetString("AppTitleUnprocessedFrames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Begin Extraction.
        /// </summary>
        public static string ExtractionBegin {
            get {
                return ResourceManager.GetString("ExtractionBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A video needs to be selected first..
        /// </summary>
        public static string ExtractionBeginError {
            get {
                return ResourceManager.GetString("ExtractionBeginError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select Extraction Location.
        /// </summary>
        public static string ExtractionBrowse {
            get {
                return ResourceManager.GetString("ExtractionBrowse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select a valid directory for the extraction location..
        /// </summary>
        public static string ExtractionLocationError {
            get {
                return ResourceManager.GetString("ExtractionLocationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Open Folder.
        /// </summary>
        public static string ExtractionOpenFolder {
            get {
                return ResourceManager.GetString("ExtractionOpenFolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pause Extraction.
        /// </summary>
        public static string ExtractionPause {
            get {
                return ResourceManager.GetString("ExtractionPause", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Resume Extraction.
        /// </summary>
        public static string ExtractionResume {
            get {
                return ResourceManager.GetString("ExtractionResume", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stop Extraction.
        /// </summary>
        public static string ExtractionStop {
            get {
                return ResourceManager.GetString("ExtractionStop", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Begin the extraction process; this can be paused..
        /// </summary>
        public static string ExtractionToolTipBegin {
            get {
                return ResourceManager.GetString("ExtractionToolTipBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a location for the extracted frames..
        /// </summary>
        public static string ExtractionToolTipBrowse {
            get {
                return ResourceManager.GetString("ExtractionToolTipBrowse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Open the folder dedicated to the extracted video frames..
        /// </summary>
        public static string ExtractionToolTipOpen {
            get {
                return ResourceManager.GetString("ExtractionToolTipOpen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pause the extraction process, can be later resumed..
        /// </summary>
        public static string ExtractionToolTipPause {
            get {
                return ResourceManager.GetString("ExtractionToolTipPause", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stop the extraction process early..
        /// </summary>
        public static string ExtractionToolTipStop {
            get {
                return ResourceManager.GetString("ExtractionToolTipStop", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Browse Video.
        /// </summary>
        public static string FileBrowseVideo {
            get {
                return ResourceManager.GetString("FileBrowseVideo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Results.csv.
        /// </summary>
        public static string FileSaveCsv {
            get {
                return ResourceManager.GetString("FileSaveCsv", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph.png.
        /// </summary>
        public static string FileSaveGraph {
            get {
                return ResourceManager.GetString("FileSaveGraph", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Frame Extract Interval {0}ms.
        /// </summary>
        public static string FrameInterval {
            get {
                return ResourceManager.GetString("FrameInterval", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Adjust the number of frames to extract. A larger interval will extract less frames from the video. To extract all frames, set the slider fully to the left..
        /// </summary>
        public static string FrameToolTipInterval {
            get {
                return ResourceManager.GetString("FrameToolTipInterval", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Show Help.
        /// </summary>
        public static string Help {
            get {
                return ResourceManager.GetString("Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Include this Frame.
        /// </summary>
        public static string ImageInclude {
            get {
                return ResourceManager.GetString("ImageInclude", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current Frame: {0} of {1}.
        /// </summary>
        public static string ImageIndex {
            get {
                return ResourceManager.GetString("ImageIndex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Load Frames.
        /// </summary>
        public static string ImageLoad {
            get {
                return ResourceManager.GetString("ImageLoad", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No images were found in this directory or they did not match this application&apos;s naming convention..
        /// </summary>
        public static string ImageLoadFail {
            get {
                return ResourceManager.GetString("ImageLoadFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Have the images already been processed by this application?.
        /// </summary>
        public static string ImageLoadQuestion {
            get {
                return ResourceManager.GetString("ImageLoadQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Next Frame.
        /// </summary>
        public static string ImageNext {
            get {
                return ResourceManager.GetString("ImageNext", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Previous Frame.
        /// </summary>
        public static string ImagePrevious {
            get {
                return ResourceManager.GetString("ImagePrevious", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Process Frames.
        /// </summary>
        public static string ImageProcess {
            get {
                return ResourceManager.GetString("ImageProcess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Display Random Frame.
        /// </summary>
        public static string ImageRandom {
            get {
                return ResourceManager.GetString("ImageRandom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skip this Frame.
        /// </summary>
        public static string ImageSkip {
            get {
                return ResourceManager.GetString("ImageSkip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Black &amp; White Threshold {0}.
        /// </summary>
        public static string ImageThreshold {
            get {
                return ResourceManager.GetString("ImageThreshold", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Toggle Black &amp; White.
        /// </summary>
        public static string ImageToggle {
            get {
                return ResourceManager.GetString("ImageToggle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Load the frames from the previous extraction folder (may be unprocessed or processed)..
        /// </summary>
        public static string ImageToolTipLoad {
            get {
                return ResourceManager.GetString("ImageToolTipLoad", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Begin to apply the black and white threshold to the extracted frames..
        /// </summary>
        public static string ImageToolTipProcess {
            get {
                return ResourceManager.GetString("ImageToolTipProcess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Display a randomly selected frame..
        /// </summary>
        public static string ImageToolTipRandom {
            get {
                return ResourceManager.GetString("ImageToolTipRandom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skip the current image from the OCR phase. Can be used for when it is certain that the OCR will not produce a value..
        /// </summary>
        public static string ImageToolTipSkip {
            get {
                return ResourceManager.GetString("ImageToolTipSkip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Change the threshold for the image black and white levels..
        /// </summary>
        public static string ImageToolTipThreshold {
            get {
                return ResourceManager.GetString("ImageToolTipThreshold", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Toggle the image between displaying in black and white or full colour..
        /// </summary>
        public static string ImageToolTipToggle {
            get {
                return ResourceManager.GetString("ImageToolTipToggle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Begin OCR.
        /// </summary>
        public static string OcrBegin {
            get {
                return ResourceManager.GetString("OcrBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Begin the optical character recognition (OCR) process..
        /// </summary>
        public static string OcrToolTipBegin {
            get {
                return ResourceManager.GetString("OcrToolTipBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter an optional expected maximum value from the video. Both ranges must be set for it to apply..
        /// </summary>
        public static string OcrToolTipValueMax {
            get {
                return ResourceManager.GetString("OcrToolTipValueMax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter an optional expected minimum value from the video. Both ranges must be set for it to apply..
        /// </summary>
        public static string OcrToolTipValueMin {
            get {
                return ResourceManager.GetString("OcrToolTipValueMin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter an optional max value….
        /// </summary>
        public static string OcrValueMax {
            get {
                return ResourceManager.GetString("OcrValueMax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter an optional min value….
        /// </summary>
        public static string OcrValueMin {
            get {
                return ResourceManager.GetString("OcrValueMin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You need to have located or extracted the frames first!.
        /// </summary>
        public static string ProcessingError {
            get {
                return ResourceManager.GetString("ProcessingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Processing Frames {0}%.
        /// </summary>
        public static string ProcessingFrames {
            get {
                return ResourceManager.GetString("ProcessingFrames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Running OCR {0}%.
        /// </summary>
        public static string ProcessingOcr {
            get {
                return ResourceManager.GetString("ProcessingOcr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Processing Video {0}%.
        /// </summary>
        public static string ProcessingVideo {
            get {
                return ResourceManager.GetString("ProcessingVideo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Return to App.
        /// </summary>
        public static string ResultsBack {
            get {
                return ResourceManager.GetString("ResultsBack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No results have a value from the OCR..
        /// </summary>
        public static string ResultsError {
            get {
                return ResourceManager.GetString("ResultsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Export to CSV File.
        /// </summary>
        public static string ResultsExportCsv {
            get {
                return ResourceManager.GetString("ResultsExportCsv", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save Graph as Image.
        /// </summary>
        public static string ResultsExportImage {
            get {
                return ResourceManager.GetString("ResultsExportImage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Finish Animation.
        /// </summary>
        public static string ResultsGraphAnimateFinish {
            get {
                return ResourceManager.GetString("ResultsGraphAnimateFinish", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph Update Interval {0}ms.
        /// </summary>
        public static string ResultsGraphAnimateInterval {
            get {
                return ResourceManager.GetString("ResultsGraphAnimateInterval", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Change the number of milliseconds it takes to update the graph..
        /// </summary>
        public static string ResultsGraphAnimateIntervalToolTip {
            get {
                return ResourceManager.GetString("ResultsGraphAnimateIntervalToolTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pause Animation.
        /// </summary>
        public static string ResultsGraphAnimatePause {
            get {
                return ResourceManager.GetString("ResultsGraphAnimatePause", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pause or resume the graph animation..
        /// </summary>
        public static string ResultsGraphAnimatePauseResumeToolTip {
            get {
                return ResourceManager.GetString("ResultsGraphAnimatePauseResumeToolTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Resume Animation.
        /// </summary>
        public static string ResultsGraphAnimateResume {
            get {
                return ResourceManager.GetString("ResultsGraphAnimateResume", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Show Animation.
        /// </summary>
        public static string ResultsGraphAnimateStart {
            get {
                return ResourceManager.GetString("ResultsGraphAnimateStart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Timestamp (Seconds).
        /// </summary>
        public static string ResultsGraphAxisX {
            get {
                return ResourceManager.GetString("ResultsGraphAxisX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value.
        /// </summary>
        public static string ResultsGraphAxisY {
            get {
                return ResourceManager.GetString("ResultsGraphAxisY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Y Axis Title.
        /// </summary>
        public static string ResultsGraphAxisYWatermark {
            get {
                return ResourceManager.GetString("ResultsGraphAxisYWatermark", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Timestamp: {0:0.00}s.
        /// </summary>
        public static string ResultsGraphTimestampTooltip {
            get {
                return ResourceManager.GetString("ResultsGraphTimestampTooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Results.
        /// </summary>
        public static string ResultsGraphTitle {
            get {
                return ResourceManager.GetString("ResultsGraphTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph Title.
        /// </summary>
        public static string ResultsGraphTitleWatermark {
            get {
                return ResourceManager.GetString("ResultsGraphTitleWatermark", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value: {0}.
        /// </summary>
        public static string ResultsGraphValueTooltip {
            get {
                return ResourceManager.GetString("ResultsGraphValueTooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Export Image Size (W x H).
        /// </summary>
        public static string ResultsImageSize {
            get {
                return ResourceManager.GetString("ResultsImageSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image Height (Pixels).
        /// </summary>
        public static string ResultsImageSizeHeight {
            get {
                return ResourceManager.GetString("ResultsImageSizeHeight", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Set the height of the graph export image in pixels. Try increasing the amount if results aren&apos;t as expected..
        /// </summary>
        public static string ResultsImageSizeHeightToolTip {
            get {
                return ResourceManager.GetString("ResultsImageSizeHeightToolTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image Width (Pixels).
        /// </summary>
        public static string ResultsImageSizeWidth {
            get {
                return ResourceManager.GetString("ResultsImageSizeWidth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Set the width of the graph export image in pixels. Try increasing the amount if results aren&apos;t as expected..
        /// </summary>
        public static string ResultsImageSizeWidthToolTip {
            get {
                return ResourceManager.GetString("ResultsImageSizeWidthToolTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Show Results.
        /// </summary>
        public static string ResultsShow {
            get {
                return ResourceManager.GetString("ResultsShow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Change View.
        /// </summary>
        public static string ResultsViewChange {
            get {
                return ResourceManager.GetString("ResultsViewChange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Change between CSV and Graph views..
        /// </summary>
        public static string ResultsViewChangeToolTip {
            get {
                return ResourceManager.GetString("ResultsViewChangeToolTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use Average Value.
        /// </summary>
        public static string UnprocessedFrameAverage {
            get {
                return ResourceManager.GetString("UnprocessedFrameAverage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Apply and Continue.
        /// </summary>
        public static string UnprocessedFrameContinue {
            get {
                return ResourceManager.GetString("UnprocessedFrameContinue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use Custom Value.
        /// </summary>
        public static string UnprocessedFrameCustom {
            get {
                return ResourceManager.GetString("UnprocessedFrameCustom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This frame was unable to be recognised by the OCR..
        /// </summary>
        public static string UnprocessedFrameFailed {
            get {
                return ResourceManager.GetString("UnprocessedFrameFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Apply and Finish.
        /// </summary>
        public static string UnprocessedFrameFinish {
            get {
                return ResourceManager.GetString("UnprocessedFrameFinish", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All frames have now been given a value..
        /// </summary>
        public static string UnprocessedFrameFinished {
            get {
                return ResourceManager.GetString("UnprocessedFrameFinished", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select what to do with this unprocessed or skipped frame - {0} of {1}..
        /// </summary>
        public static string UnprocessedFrameIndex {
            get {
                return ResourceManager.GetString("UnprocessedFrameIndex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use Next Value.
        /// </summary>
        public static string UnprocessedFrameNext {
            get {
                return ResourceManager.GetString("UnprocessedFrameNext", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use Previous Value.
        /// </summary>
        public static string UnprocessedFramePrevious {
            get {
                return ResourceManager.GetString("UnprocessedFramePrevious", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This frame was assigned a value of {0}.
        ///
        ///This is outside of the given minimum ({1}) and maximum ({2}) range..
        /// </summary>
        public static string UnprocessedFrameRange {
            get {
                return ResourceManager.GetString("UnprocessedFrameRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Repeat for all {0} Frames.
        /// </summary>
        public static string UnprocessedFrameRepeat {
            get {
                return ResourceManager.GetString("UnprocessedFrameRepeat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This frame was chosen by you to be skipped, as it was assumed that the OCR wouldn&apos;t be able to interpret it..
        /// </summary>
        public static string UnprocessedFrameSkipped {
            get {
                return ResourceManager.GetString("UnprocessedFrameSkipped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Choose what to do with skipped frames and frames that cannot be interpreted..
        /// </summary>
        public static string UnprocessedFrameToolTip {
            get {
                return ResourceManager.GetString("UnprocessedFrameToolTip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a Video.
        /// </summary>
        public static string VideoBrowse {
            get {
                return ResourceManager.GetString("VideoBrowse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Duration: {0}.
        /// </summary>
        public static string VideoDuration {
            get {
                return ResourceManager.GetString("VideoDuration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Frames per Second: {0}.
        /// </summary>
        public static string VideoFPS {
            get {
                return ResourceManager.GetString("VideoFPS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to C:\Users\Example\Videos\Example.mp4.
        /// </summary>
        public static string VideoLocationPath {
            get {
                return ResourceManager.GetString("VideoLocationPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Open Folder.
        /// </summary>
        public static string VideoOpenFolder {
            get {
                return ResourceManager.GetString("VideoOpenFolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Resolution: {0} x {1}.
        /// </summary>
        public static string VideoResolution {
            get {
                return ResourceManager.GetString("VideoResolution", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Size: {0} MB.
        /// </summary>
        public static string VideoSize {
            get {
                return ResourceManager.GetString("VideoSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name: {0}.
        /// </summary>
        public static string VideoTitle {
            get {
                return ResourceManager.GetString("VideoTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Open the folder containing the selected video..
        /// </summary>
        public static string VideoToolTipOpen {
            get {
                return ResourceManager.GetString("VideoToolTipOpen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a video (of type MP4) to process into a graph..
        /// </summary>
        public static string VideoToolTipSelect {
            get {
                return ResourceManager.GetString("VideoToolTipSelect", resourceCulture);
            }
        }
    }
}
