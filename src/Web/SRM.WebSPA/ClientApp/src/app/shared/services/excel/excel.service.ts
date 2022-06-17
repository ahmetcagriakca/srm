import { Injectable } from '@angular/core';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';

const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

@Injectable()
export class ExcelService {

	constructor() { }

	public exportAsExcelFile(json: any[], excelFileName: string): void {

		const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json);
		console.log('worksheet', worksheet);
		const workbook: XLSX.WorkBook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
		const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
		//const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'buffer' });
		this.saveAsExcelFile(excelBuffer, excelFileName);
	}
	assign(obj, prop, value) {
		if (typeof prop === "string")
			prop = prop.split(".");

		if (prop.length > 1) {
			var e = prop.shift();
			this.assign(obj[e] =
				Object.prototype.toString.call(obj[e]) === "[object Object]"
					? obj[e]
					: {},
				prop,
				value);
		} else
			obj[prop[0]] = value;
	}

	public exportAsExcelFileWithSubArray(json: any[], subArrayName: any, subArraySheetNames: any[], excludedFields: any[], excelFileName: string): void {
		const test = json.map(a => Object.assign({}, a));//Array.from(Object.create(json));//Array.from();
		for (let k = 0; k < test.length; k++) {
			const t = test[k];
			for (let j = 0; j < excludedFields.length; j++) {
				const excludedField = excludedFields[j];
				delete t[excludedField];
			}
		}
		const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(test);
		var sheetNames = [];
		var sheets = {};
		sheets[excelFileName] = worksheet;
		sheetNames.push(excelFileName);
		for (let index = 0; index < json.length; index++) {
			const element = json[index];
			const worksheetArrays: XLSX.WorkSheet = XLSX.utils.json_to_sheet(element[subArrayName]);
			var sheetName = "";
			for (let i = 0; i < subArraySheetNames.length; i++) {
				const subArraySheetName = subArraySheetNames[i];
				if (subArraySheetNames.length == i + 1) {
					sheetName += element[subArraySheetName];
				} else {
					sheetName += element[subArraySheetName] + "_";
				}
			}
			this.assign(sheets, sheetName, worksheetArrays);
			sheetNames.push(sheetName);
		}
		// const worksheetArrays: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json[0].students);
		console.log('worksheet', sheets);
		console.log('worksheet', sheetNames);
		const workbook: XLSX.WorkBook = { Sheets: sheets, SheetNames: sheetNames };
		const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
		//const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'buffer' });
		this.saveAsExcelFile(excelBuffer, excelFileName);
	}


	private saveAsExcelFile(buffer: any, fileName: string): void {
		const data: Blob = new Blob([buffer], {
			type: EXCEL_TYPE
		});
		FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
	}

}