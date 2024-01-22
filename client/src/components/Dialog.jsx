import React from "react";
import Dialog from "@mui/material/Dialog";
import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogActions from "@mui/material/DialogActions";
import Button from "@mui/material/Button";

const PrimaryDialog = ({ title, content, open, handleClose, onClick }) => {
  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>{title}</DialogTitle>
      <DialogContent>
        <p>{content}</p>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClick} color="primary">
          Bekreft
        </Button>
        <Button onClick={handleClose} color="primary">
          Avbryt
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default PrimaryDialog;
