import { Injectable } from '@angular/core';
import { Message } from 'primeng/primeng';

@Injectable()
export class NotificationService {
	messages: Message[];

	constructor() {
		this.messages = [];
	}

	add(message: Message) {
		if (message) {
			this.messages.push(message);
		}
	}
	success(detail: string, summary?: string): void {
		this.messages.push({
			severity: 'success', summary: summary, detail: detail
		});
	}

	info(detail: string, summary?: string): void {
		this.messages.push({
			severity: 'info', summary: summary, detail: detail
		});
	}

	warning(detail: string, summary?: string): void {
		this.messages.push({
			severity: 'warn', summary: summary, detail: detail
		});
	}

	error(detail: string, summary?: string): void {
		this.messages.push({
			severity: 'error', summary: summary, detail: detail
		});
	}
}