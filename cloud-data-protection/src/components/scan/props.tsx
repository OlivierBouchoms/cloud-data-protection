import {FileUploadScanInfoOutput} from "services/result/demo/fileUploadResult";

export interface ScanProps {
    open: boolean;
    onClose: () => void;
    scanInfo: FileUploadScanInfoOutput;
}
