import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

type ButtonVariant = 'primary' | 'secondary' | 'danger' | 'ghost' | 'success';
type ButtonSize = 'sm' | 'md' | 'lg';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [CommonModule],
  template: `
    <button
      [class]="computedClasses"
      [disabled]="disabled"
      [type]="type"
    >
      <ng-content />
    </button>
  `
})

export class Button {
  @Input() variant: ButtonVariant = 'primary';
  @Input() size: ButtonSize = 'md';
  @Input() disabled = false;
  @Input() type: 'button' | 'submit' | 'reset' = 'button';
  @Input() extraClasses = '';

  private readonly baseClasses =
    'inline-flex items-center justify-center font-medium transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed';

  private readonly variantClasses: Record<ButtonVariant, string> = {
    primary:   'p-2 h-full text-xl hover:bg-white transition',
    secondary: 'bg-gray-200 text-gray-800 hover:bg-gray-300 focus:ring-gray-400',
    danger:    'bg-red-600 text-white hover:bg-red-700 focus:ring-red-500',
    ghost:     'bg-transparent text-gray-700 hover:bg-gray-100 focus:ring-gray-300',
    success:   'bg-green-600 text-white hover:bg-green-700 focus:ring-green-500',
  };

  private readonly sizeClasses: Record<ButtonSize, string> = {
    sm: 'text-sm px-3 py-1.5',
    md: 'text-sm px-4 py-2',
    lg: 'text-base px-6 py-3',
  };

  get computedClasses(): string {
    return [
      this.baseClasses,
      this.variantClasses[this.variant],
      this.sizeClasses[this.size],
      this.extraClasses,
    ].join(' ');
  }
}
