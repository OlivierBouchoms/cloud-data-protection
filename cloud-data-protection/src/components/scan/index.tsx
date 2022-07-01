import {Dialog, LinearProgress} from "@material-ui/core";
import {ScanProps} from "components/scan/props";
import React, {useEffect, useRef, useState} from "react";
import './index.css';

const Scan = (props: ScanProps) => {
    const scanReportRef = useRef<HTMLIFrameElement>();
    const [scanReportInitialised, setScanReportInitialised] = useState<boolean>(false);

    const [reportLoading, setReportLoading] = useState<boolean>(true);

    const {open, onClose} = props;

    useEffect(() => {
        if (open && scanReportRef.current) {
            onOpenReport();
        }
    }, [open, scanReportInitialised])

    const onOpenReport = async () => {
        const widgetUrl = props.scanInfo?.widgetUrl;

        setReportLoading(true);

        const iframe = scanReportRef.current! as HTMLIFrameElement;

        iframe.style.height = 'unset';

        const content = await fetch(widgetUrl)
            .then(res => res.text());

        if (!iframe.contentWindow) {
            return;
        }

        iframe.contentWindow.document.open();
        iframe.contentWindow.document.write(content);
        iframe.contentWindow.document.close();

        iframe.contentWindow.onload = () => {
            if (!iframe?.contentWindow) {
                // Modal is already closed
                return;
            }

            const height = iframe.contentWindow.document.body.scrollHeight;

            iframe.style.height = height + 'px';

            const closeButton = iframe.contentWindow.document.querySelector('nav .vt-close-widget') as HTMLButtonElement;

            if (closeButton) {
                closeButton.onclick = () => props.onClose();
            }

            setReportLoading(false);
        };
    }

    return (
        <Dialog open={open} onClose={onClose} fullWidth={true} maxWidth='lg' className='dialog--scan-info' scroll='paper'>
            {reportLoading && <LinearProgress />}
            <div className='dialog__content dialog__content--scan'>
                <div className='scan-info__report'>
                    <iframe ref={el => { scanReportRef.current = el!; setScanReportInitialised(!!el) }} src='about:blank' title='Scan results'/>
                </div>
            </div>
        </Dialog>
    )
}

export default Scan;